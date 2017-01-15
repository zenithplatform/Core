using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop;
using Zenith.Core.Interop.Message;

namespace Zenith.Core.Runtime.Processing
{
    public class LocalRequest
    {
        PyBridge _bridge = null;
        IBridgeCallback _callback = null;
        
        public LocalRequest()
        {
            _bridge = new PyBridge("tcp://localhost:18801");
        }

        public LocalRequest(IBridgeCallback callback)
            :this()
        {
            _callback = callback;
            _bridge.Open(_callback);
        }

        public void Send(ProcessingInput input)
        {
            if (!_bridge.Ready)
                _bridge.Open(_callback);

            _bridge.Send(input);
        }
    }
}
