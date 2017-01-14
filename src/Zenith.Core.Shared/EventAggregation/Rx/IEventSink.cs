using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation.Rx
{
    public interface IEventSink
    {
        void Publish<TEvent>(TEvent sampleEvent);
        IObservable<TEvent> GetEvent<TEvent>();
    }
}
