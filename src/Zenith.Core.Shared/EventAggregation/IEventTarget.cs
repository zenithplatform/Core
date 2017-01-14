using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public interface IEventTarget
    {
        void Register(IEventAggregator aggregator);
    }
}
