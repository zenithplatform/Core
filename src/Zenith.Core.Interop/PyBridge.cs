using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop.Message;
using ZeroMQ;

namespace Zenith.Core.Interop
{
    public class PyBridge
    {
        ZmqSocket _socket = null;
        string _address = string.Empty;
        volatile bool _ready = false;

        public PyBridge(string address)
        {
            _address = address;

            var context = ZmqContext.Create();
            _socket = context.CreateSocket(SocketType.PUSH);
        }

        public bool Open(IBridgeCallback callback)
        {
            if (string.IsNullOrEmpty(_address))
            {
                _ready = false;
                return _ready;
            }

            try
            {
                _socket.Connect(_address);

                if (callback != null)
                    callback.Wait();

                _ready = true;
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

        public bool Open()
        {
            return Open(null);
        }

        public bool Send(MessageBase message)
        {
            SendStatus status = SendStatus.None;

            try
            {
                status = _socket.Send(message.JsonData, Encoding.UTF8);
            }
            catch (Exception exc)
            {
                _ready &= false;
            }
            finally
            {
                _ready &= (status == SendStatus.Sent);
            }

            return _ready;
        }

        public bool Ready
        {
            get { return _ready; }
        }
    }
}
