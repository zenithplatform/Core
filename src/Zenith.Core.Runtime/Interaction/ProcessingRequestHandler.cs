using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Runtime.Infrastructure;

namespace Zenith.Core.Runtime.Processing
{
    public class ProcessingRequestHandler
    {
        IPipeline _pipeline = null;
        
        public ProcessingRequestHandler(IPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public void Send<T>(PipelineMessage<T> input)
        {
            _pipeline.Send(input);
        }
    }
}
