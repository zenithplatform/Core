using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Caching
{
    public abstract class CacheInstanceBase<T> where T : class
    {
        public abstract void Initialize();

        public abstract bool AddOrUpdate(string key, T value);
        public abstract T Get(string key);
        public abstract bool Remove(string key);

        public ICacheManager<T> CacheManager { get; }
    }
}
