using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Interop;
using ZeroMQ;

namespace Zenith.Core.Interop
{
    public interface IPyBridge
    {
        bool Open();
        bool Send(MessageBase message);
        IBridgeCallback CreateCallback<T>() where T : IBridgeCallback;
        bool Ready { get; }
    }

    public class PyBridge : IPyBridge
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

        public bool Open()
        {
            if (string.IsNullOrEmpty(_address))
            {
                _ready = false;
                return _ready;
            }

            try
            {
                _socket.Connect(_address);
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

        public IBridgeCallback CreateCallback<T>() where T : IBridgeCallback
        {
            //object[] args = new object[] { "tcp://localhost:18800" , null};

            //object instance = Activator.CreateInstance(typeof(T), args);
            //return (IBridgeCallback)instance;
            return Activator.CreateInstance<T>();
        }

        public bool Ready
        {
            get { return _ready; }
        }
    }
}
