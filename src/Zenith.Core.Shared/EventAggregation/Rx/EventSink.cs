using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation.Rx
{
    public class EventSink : IEventSink
    {
        private readonly ConcurrentDictionary<Type, object> subjects = new ConcurrentDictionary<Type, object>();

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject = (ISubject<TEvent>)subjects.GetOrAdd(typeof(TEvent), t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;

            if (subjects.TryGetValue(typeof(TEvent), out subject))
            {
                ((ISubject<TEvent>)subject).OnNext(sampleEvent);
            }
        }
    }
}
