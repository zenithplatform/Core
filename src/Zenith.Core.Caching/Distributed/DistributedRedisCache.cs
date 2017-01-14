using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Caching.Distributed
{
    public class DistributedRedisCache<T> : CacheInstanceBase<T> where T : class
    {
        public override bool AddOrUpdate(string key, T value)
        {
            throw new NotImplementedException();
        }

        public override T Get(string key)
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override bool Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
