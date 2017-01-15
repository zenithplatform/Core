using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Interop.Message;
using Zenith.Core.Shared.EventAggregation;
using ZeroMQ;

namespace Zenith.Core.Interop
{
    public abstract class BridgeCallback : IBridgeCallback
    {
        private string _endpoint = string.Empty;
        private Thread _workerThread;
        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);
        private readonly object _locker = new object();
        private readonly Queue<string> _queue = new Queue<string>();
        IEventAggregator _aggregator = null;

        public BridgeCallback(string endPoint, IEventAggregator aggregator)
        {
            _endpoint = endPoint;
            _aggregator = aggregator;
        }

        public void AddJsonHandler<T>(IJsonCallbackHandler handler)
        {
            Action<string> action = handler.OnReceiveJson;
            _aggregator.Subsribe(action);
        }

        private void Start()
        {
            ZmqContext context = ZmqContext.Create();

            _workerThread = new Thread(Receive);
            _workerThread.Start(context);
        }

        private void Stop()
        {
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
                        Thread.Sleep(100);

                        int received = socket.Receive(buffer);

                        if (received <= 0)
                            continue;

                        var message = string.Empty;

                        using (var stream = new MemoryStream(buffer, 0, received))
                        {
                            message = Encoding.UTF8.GetString(stream.ToArray());
                        }

                        if (string.IsNullOrEmpty(message))
                            continue;

                        lock (_locker)
                        {
                            _queue.Enqueue(message);
                            _aggregator.Publish(message);
                        }
                    }
                }
                catch (Exception exc)
                {

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
            this.Stop();
        }
    }
}
