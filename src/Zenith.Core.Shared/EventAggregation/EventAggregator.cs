using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public class EventAggregator : IEventAggregator
    {
        IDictionary<Type, IList<EventHandler<IEvent>>> handlers;

        public EventAggregator()
        {
            handlers = new Dictionary<Type, IList<EventHandler<IEvent>>>();
        }

        public void Register(IEventTarget target)
        {
            target.Register(this);
        }

        public void Register<T>(EventHandler<T> handler) where T : IEvent
        {
            if (!handlers.ContainsKey(typeof(T)))
            {
                handlers[typeof(T)] = new List<EventHandler<IEvent>>();
            }

            var handlerList = handlers[typeof(T)];

            handlerList.Add(evt => handler((T)evt));
        }

        public void Trigger(IEvent evt)
        {
            IList<EventHandler<IEvent>> handlerList;

            if (handlers.TryGetValue(evt.GetType(), out handlerList))
            {
                foreach (EventHandler<IEvent> handler in handlerList)
                {
                    handler.Invoke(evt);
                }
            }
        }
    }
}
