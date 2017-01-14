using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders
{
    public class DataStorageResult
    {
        public bool Success { get; set; }
        public Exception Error { get; set; }
    }

    public class DataStorageResult<T> : DataStorageResult where T : class
    {
        public T Context { get; set; }
    }
}
