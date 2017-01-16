using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop;
using Zenith.Core.Models.Runtime;

namespace Zenith.Core.Runtime.Processing
{
    public class ProcessingRequest
    {
        IPyBridge _bridge = null;
        
        public ProcessingRequest(IPyBridge bridge)
        {
            _bridge = bridge;
        }

        public void Send(ProcessingInput input)
        {
            if (!_bridge.Ready)
                _bridge.Open();

            _bridge.Send(input);
        }
    }
}
