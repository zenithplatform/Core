using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public interface IEventAggregator : IDisposable
    {
		void Publish<T>(T data);
        void Publish<T>(T data, string routingKey);
        string Subsribe<T>(Action<T> action);
        string Subsribe<T>(Action<T> action, string routingKey);
        void Unsubscribe(string token);
        void Clear();
    }
}
