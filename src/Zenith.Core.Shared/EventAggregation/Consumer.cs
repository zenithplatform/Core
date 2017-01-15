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

        internal Consumer(object handler, Type type)
        {
            _handler = handler;
            _type = type;
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
    }
}
