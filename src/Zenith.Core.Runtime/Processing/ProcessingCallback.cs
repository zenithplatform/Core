using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop;
using Zenith.Core.Shared.EventAggregation;

namespace Zenith.Core.Runtime.Processing
{
    public class ProcessingCallback : BridgeCallback<ProcessingOutput>
    {
        public ProcessingCallback()
            : base("tcp://localhost:18800", new ProcessingCallbackHandler(), EventAggregator.Instance)
        {
            
        }
    }
}
