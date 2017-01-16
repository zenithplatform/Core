using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    internal class Consumer
    {
        object _handler = null;
        Type _type = null;
        string _routingKey = string.Empty;

        internal Consumer(object handler, Type type)
        {
            _handler = handler;
            _type = type;
        }

        internal Consumer(object handler, Type type, string routingKey)
            :this(handler, type)
        {
            _handler = handler;
            _type = type;
            _routingKey = routingKey;
        }

        internal void Fire<T>(T data)
        {
            var handler = Handler as Action<T>;
            handler(data);
        }

        internal object Handler
        {
            get { return _handler; }
        }

        internal Type ConsumerType
        {
            get { return _type; }
        }

        internal string RoutingKey
        {
            get { return _routingKey; }
        }
    }
}
