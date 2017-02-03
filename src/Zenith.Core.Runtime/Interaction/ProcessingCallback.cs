using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Runtime.Infrastructure;

namespace Zenith.Core.Runtime.Processing
{
    public class DefaultProcessingCallback : PipelineCallback
    {
        public DefaultProcessingCallback()
            : base("tcp://localhost:18800")
        {
            base.AddJsonHandler<ProcessingOutput>(new TracingCallbackHandler());
        }
    }
}
