using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Shared.EventAggregation;
using ZeroMQ;

namespace Zenith.Core.Interop
{
    public abstract class BridgeCallback : IBridgeCallback
    {
        private readonly string _endpoint = string.Empty;
        private Thread _workerThread;
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        private readonly object _locker = new object();
        private readonly Queue<string> _queue = new Queue<string>();
        private readonly IEventAggregator _aggregator = null;
        private readonly List<string> _subscriptions = new List<string>();
        private readonly List<IJsonMessagePreProcessor> _jsonPreProcessors = new List<IJsonMessagePreProcessor>();
        private volatile bool _isActive = false;

        public BridgeCallback(string endPoint, IEventAggregator aggregator)
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
            ZmqContext context = ZmqContext.Create();

            _workerThread = new Thread(Receive);
            _workerThread.Start(context);
            _isActive = true;
        }

        private void Stop()
        {
            _isActive = false;
            _stopEvent.Set();
            _workerThread.Join();
        }

        private void Receive(object context)
        {
            var buffer = new byte[4096];
            ZmqContext zContext = (ZmqContext)context;

            if (zContext == null)
                return;

            using (var socket = zContext.CreateSocket(SocketType.SUB))
            {
                try
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

                        Thread.Sleep(10);

                        lock (_locker)
                        {
                            int received = socket.Receive(buffer);

                            if (received <= 0)
                                continue;

                            string message = string.Empty;

                            using (var stream = new MemoryStream(buffer, 0, received))
                            {
                                message = Encoding.UTF8.GetString(stream.ToArray());
                            }

                            if (string.IsNullOrEmpty(message))
                                continue;

                            _queue.Enqueue(message);
                            string jsonData = string.Empty;

                            if(_jsonPreProcessors.Count == 0)
                            {
                                DefaultJsonMessagePreProcessor defaultPreProcessor = new DefaultJsonMessagePreProcessor();
                                jsonData = defaultPreProcessor.PreProcess(message);
                            }

                            if (string.IsNullOrEmpty(jsonData))
                                continue;

                            _aggregator.Publish(jsonData);
                        }
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.ToString());
                }
                finally
                {
                    socket.Dispose();
                    zContext.Dispose();
                }
            }
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

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }
    }
}
