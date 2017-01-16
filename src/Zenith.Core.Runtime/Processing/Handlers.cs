using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop;
using Zenith.Core.Models.Runtime;

namespace Zenith.Core.Runtime.Processing
{
    public class TracingCallbackHandler : JsonCallbackHandler<ProcessingOutput>
    {
        protected override void MessageReceived(ProcessingOutput obj)
        {
            Trace.WriteLine(obj);
        }
    }
}
