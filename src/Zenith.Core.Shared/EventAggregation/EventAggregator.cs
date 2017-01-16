using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public class EventAggregator : IEventAggregator
    {
        private static readonly object _syncObject = new object();
        private static readonly object _collSyncObject = new object();
        private readonly Dictionary<string, Consumer> _all = new Dictionary<string, Consumer>();

        public void Publish<T>(T data)
        {
            Publish(data, string.Empty);
        }

        public void Publish<T>(T data, string routingKey)
        {
            IEnumerable<Consumer> consumers = null;

            if (!string.IsNullOrEmpty(routingKey))
                consumers = FindByRoutingKey(routingKey);
            else
                consumers = FindByType(typeof(T));

            if (consumers == null || consumers.Count() == 0)
                return;

            foreach (Consumer consumer in consumers)
            {
                if (consumer != null)
                    consumer.Fire<T>(data);
            }
        }

        public string Subsribe<T>(Action<T> action)
        {
            return Subsribe(action, string.Empty);
        }

        public string Subsribe<T>(Action<T> action, string routingKey)
        {
            Consumer consumer = null;
            string identifier = string.Empty;

            if (string.IsNullOrEmpty(routingKey))
                consumer = new Consumer(action, typeof(T));
            else
                consumer = new Consumer(action, typeof(T), routingKey);

            lock (_collSyncObject)
            {
                identifier = GetIdentifier();
                _all.Add(identifier, consumer);
            }

            return identifier;
        }

        public void Unsubscribe(string token)
        {
            RemoveByToken(token);
        }

        public void Clear()
        {
            lock (_collSyncObject) { _all.Clear(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Consumer> FindByType(Type type)
        {
            try
            {
                lock (_collSyncObject)
                {
                    return _all.Values.Where(it => it.ConsumerType.IsAssignableFrom(type));
                }
            }
            catch { return null; }
        }

        private IEnumerable<Consumer> FindByRoutingKey(string routingKey)
        {
            try
            {
                lock (_collSyncObject)
                {
                    return _all.Values.Where(it => it.RoutingKey.Equals(routingKey));
                }
            }
            catch { return null; }
        }

        private Consumer FindByToken(string token)
        {
            lock (_collSyncObject)
            {
                if (_all.ContainsKey(token))
                    return _all[token];
            }

            return null;
        }

        private void RemoveByToken(string token)
        {
            lock (_collSyncObject)
            {
                if (_all.ContainsKey(token))
                    _all.Remove(token);
            }
        }

        private string GetIdentifier()
        {
            return DateTime.Now.Ticks.ToString("x");
        }
    }
}
