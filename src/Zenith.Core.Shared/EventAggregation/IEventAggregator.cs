using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public interface IEventAggregator
    {
		void Register(IEventTarget target);
        void Register<T>(EventHandler<T> handler) where T : IEvent;
        void Trigger(IEvent evt);
    }
}
