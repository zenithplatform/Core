using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zenith.Core.Shared.EventAggregation;
using ZeroMQ;

namespace Zenith.Core.Runtime.Infrastructure
{
    public class PipelineCallback : IPipelineCallback
    {
        private readonly string _endpoint = string.Empty;
        private Task _receiveTask;
        private ZmqContext _context = null;
        private ZmqSocket _socket = null;
        private CancellationTokenSource _source;
        private CancellationToken _token;
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        private readonly object _locker = new object();
        private readonly Queue<string> _queue = new Queue<string>();
        private readonly IEventAggregator _aggregator = null;
        private readonly List<string> _subscriptions = new List<string>();
        private readonly List<IJsonMessagePreProcessor> _jsonPreProcessors = new List<IJsonMessagePreProcessor>();
        private volatile bool _isActive = false;
        private bool _disposed = false;

        public PipelineCallback(string endPoint, IEventAggregator aggregator = null)
        {
            _endpoint = endPoint;

            if (aggregator == null)
                _aggregator = new EventAggregator();
            else
                _aggregator = aggregator;
        }

        public void AddJsonHandler<T>(IJsonCallbackHandler handler)
        {
            Action<string> action = handler.OnReceiveJson;
            string routingKey = string.Empty;

            string token = _aggregator.Subsribe(action, routingKey);

            _subscriptions.Add(token);
        }

        public void AddMessagePreProcessor(IJsonMessagePreProcessor preProcessor)
        {
            _jsonPreProcessors.Add(preProcessor);
        }

        private void Start()
        {
            this.StartReceiving();
            _isActive = true;
        }

        private void Stop()
        {
            _isActive = false;
            _source.Cancel();
            _stopEvent.WaitOne();
            _stopEvent.Close();
        }

        private void StartReceiving()
        {
            var buffer = new byte[4096];

            _receiveTask = Task.Run(() =>
            {
                try
                {
                    using (ZmqContext context = ZmqContext.Create())
                    using (ZmqSocket socket = context.CreateSocket(SocketType.SUB))
                    {
                        socket.SubscribeAll();
                        socket.Connect(_endpoint);

                        while (!_stopEvent.WaitOne(0))
                        {
                            //var frames = new List<Frame>();
                            //do
                            //{
                            //    frames.Add(socket.ReceiveFrame());
                            //}
                            //while (frames.Last().HasMore);

                            lock (_locker)
                            {
                                int received = socket.Receive(buffer);

                                if (received <= 0)
                                    continue;

                                OnMessageReceived(buffer, received);

                                if (_token.IsCancellationRequested)
                                    _token.ThrowIfCancellationRequested();
                            }
                        }
                    }
                }
                catch (OperationCanceledException cExc) { _stopEvent.Set(); }
                catch (Exception exc) { }

            }, _token);
        }

        private void OnMessageReceived(byte[] buffer, int bytesReceived)
        {
            string message = string.Empty;

            using (var stream = new MemoryStream(buffer, 0, bytesReceived))
            {
                message = Encoding.UTF8.GetString(stream.ToArray());
            }

            if (string.IsNullOrEmpty(message))
                return;

            _queue.Enqueue(message);
            string jsonData = string.Empty;

            //Should be removed
            if (_jsonPreProcessors.Count == 0)
            {
                DefaultJsonMessagePreProcessor defaultPreProcessor = new DefaultJsonMessagePreProcessor();
                jsonData = defaultPreProcessor.PreProcess(message);
            }

            if (string.IsNullOrEmpty(jsonData))
                return;

            _aggregator.Publish(jsonData);
        }

        public void Activate()
        {
            this.Start();
        }

        public void Deactivate()
        {
            foreach (string token in _subscriptions)
                _aggregator.Unsubscribe(token);

            this.Stop();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (this._receiveTask != null)
                    {
                        Stop();

                        if (this._receiveTask.Status == TaskStatus.RanToCompletion ||
                            this._receiveTask.Status == TaskStatus.Faulted ||
                            this._receiveTask.Status == TaskStatus.Canceled)
                        {
                            this._receiveTask.Dispose();
                            this._receiveTask = null;
                        }
                    }

                    if (this._context != null)
                    {
                        this._context.Dispose();
                        this._context = null;
                    }

                    if (this._socket != null)
                    {
                        this._socket.Dispose();
                        this._socket = null;
                    }

                    if (this._source != null)
                    {
                        this._source.Dispose();
                        this._source = null;
                    }
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PipelineCallback()
        {
            Dispose(false);
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }
    }
}
