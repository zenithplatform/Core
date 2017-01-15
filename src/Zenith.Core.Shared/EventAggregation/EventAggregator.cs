using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public class EventAggregator : IEventAggregator
    {
        private static EventAggregator _instance = new EventAggregator();
        private static object _syncObject = new object();
        private static readonly Dictionary<string, Consumer> _all = new Dictionary<string, Consumer>();

        private EventAggregator() { }
        static EventAggregator() { }

        public void Publish<T>(T data)
        {
            Consumer consumer = null;

            foreach(KeyValuePair<string, Consumer> kvPair in _all)
            {
                consumer = kvPair.Value;
                if (!consumer.ConsumerType.IsAssignableFrom(typeof(T))) { continue; }

                consumer.Fire<T>(data);
            }
        }

        public void Subsribe<T>(Action<T> action)
        {
            Consumer consumer = new Consumer(action, typeof(T));
            string key = GetIdentifier();

            _all.Add(key, consumer);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public static EventAggregator Instance
        {
            get
            {
                lock(_syncObject)
                {
                    if (_instance == null)
                        _instance = new EventAggregator();
                }

                return _instance;
            }
        }

        private string GetIdentifier()
        {
            return DateTime.Now.Ticks.ToString("x");
        }
    }
}
