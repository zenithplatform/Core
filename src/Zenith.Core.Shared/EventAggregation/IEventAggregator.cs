using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.EventAggregation
{
    public interface IEventAggregator:IDisposable
    {
		void Publish<T>(T data);
        void Subsribe<T>(Action<T> action);
        void Clear();
    }
}
