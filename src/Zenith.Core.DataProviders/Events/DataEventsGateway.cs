using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Shared.EventAggregation;

namespace Zenith.Core.DataProviders.Events
{
    public class DataEventsGateway
    {
        private static object _syncLock = new object();
        private static IEventAggregator _aggregator = null;

        public static IEventAggregator Instance
        {
            get
            {
                if (_aggregator == null)
                {
                    lock (_syncLock)
                    {
                        if (_aggregator == null)
                            _aggregator = new EventAggregator();
                    }
                }

                return _aggregator;
            }
        }
    }
}
