using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Runtime;

namespace Zenith.Core.Runtime.Infrastructure
{
    public interface IPipeline
    {
        bool Open();
        bool Send<T>(PipelineMessage<T> message);
        IPipelineCallback CreateCallback<T>() where T : IPipelineCallback;
        bool Ready { get; }
    }
}
