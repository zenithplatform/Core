using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Runtime.Infrastructure;

namespace Zenith.Core.Runtime.Processing
{
    public class TracingCallbackHandler : JsonCallbackHandler<ProcessingOutput>
    {
        protected override void MessageReceived(ProcessingOutput obj)
        {
            Trace.WriteLine(string.Format("[TracingCallbackHandler] Message : {0}", obj));
        }

        protected override void OnError(Exception exc)
        {
            Trace.WriteLine(string.Format("[TracingCallbackHandler] Error while processing message : {0}", exc));
        }
    }
}
