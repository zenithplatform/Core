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
            var context = ZmqContext.Create();
            _socket = context.CreateSocket(SocketType.PUSH);

            if (_socket != null)
                _ready = true;

            _address = address;
        }

        public bool Open()
        {
            if (!string.IsNullOrEmpty(_address))
                return false;

            bool result = false;

            try
            {
                _socket.Connect(_address);
                result = true;
            }
            catch(ZmqSocketException zexc)
            {
                
            }
            catch(ObjectDisposedException odexc)
            {

            }
            finally
            {
                _ready &= result;
            }

            return result;
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

            return (status == SendStatus.Sent);
        }

        public bool Ready
        {
            get { return _ready; }
        }
    }
}
