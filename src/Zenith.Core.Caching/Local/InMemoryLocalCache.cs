using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Caching.Local
{
    public class InMemoryLocalCache<T> : CacheInstanceBase<T> where T : class
    {
        ICacheManager<T> _manager = null;

        public override bool AddOrUpdate(string key, T value)
        {
            return _manager.Add(key, value);
        }

        public override T Get(string key)
        {
            return _manager.Get(key);
        }

        public override void Initialize()
        {
            _manager = CacheFactory.Build<T>(p => p.WithSystemRuntimeCacheHandle());
        }

        public override bool Remove(string key)
        {
            return _manager.Remove(key);
        }
    }
}
