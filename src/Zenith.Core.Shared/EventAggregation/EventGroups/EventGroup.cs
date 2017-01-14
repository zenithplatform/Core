using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation.EventGroups
{
    public static class EventGroup
    {
        static Dictionary<string, IEventAggregator> _groups = new Dictionary<string, IEventAggregator>();

        public static IEventAggregator CreateNew(string key, Func<IEventAggregator> activator)
        {
            IEventAggregator instance = activator();
            _groups.Add(key, instance);

            return instance;
        }
    }
}
