using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Shared.Serialization;
using ZeroMQ;

namespace Zenith.Core.Runtime.Infrastructure
{
    public class Pipeline : IPipeline, IDisposable
    {
        ZmqContext _context = null;
        ZmqSocket _socket = null;

        private static readonly object _sync = new object();
        string _address = string.Empty;
        volatile bool _ready = false;
        bool _disposing = false;

        public Pipeline(string address)
        {
            _address = address;
        }

        public bool Open()
        {
            if (string.IsNullOrEmpty(_address))
            {
                _ready = false;
                return _ready;
            }

            try
            {
                lock (_sync)
                {
                    _context = ZmqContext.Create();
                    _socket = _context.CreateSocket(SocketType.PUSH);
                    _socket.Connect(_address);
                    _ready = true;
                }
            }
            catch (ZmqSocketException zexc)
            {
                _ready = false;
            }
            catch (ObjectDisposedException odexc)
            {
                _ready = false;
            }

            return _ready;
        }

        public IPipelineCallback CreateCallback<T>() where T : IPipelineCallback
        {
            //object[] args = new object[] { "tcp://localhost:18800" , null};

            //object instance = Activator.CreateInstance(typeof(T), args);
            //return (IBridgeCallback)instance;
            return Activator.CreateInstance<T>();
        }

        public void Dispose()
        {
            try
            {
                if (_socket != null)
                {
                    _socket.Disconnect(_address);
                    _socket.Dispose();
                }

                if (_context != null)
                    _context.Dispose();
            }
            catch { }
        }

        public bool Send<T>(PipelineMessage<T> message)
        {
            bool sent = false;
            SendStatus status = SendStatus.None;

            try
            {
                if (!_ready)
                {
                    bool openStatus = this.Open();

                    if (!openStatus)
                        return false;
                }

                lock (_sync)
                {
                    string rawMessage = SerializationHelper.Serialize(message);
                    status = _socket.Send(rawMessage, Encoding.UTF8);
                }
            }
            catch (Exception exc)
            {
                sent = false;
            }
            finally
            {
                sent = (status == SendStatus.Sent);
            }

            return sent;
        }

        public bool Ready
        {
            get { return _ready; }
        }
    }
}
