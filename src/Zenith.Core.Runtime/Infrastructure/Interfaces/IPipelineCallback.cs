using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Runtime.Infrastructure
{
    public interface IPipelineCallback
    {
        void Activate();
        void Deactivate();
        bool IsActive { get; }
    }
}
