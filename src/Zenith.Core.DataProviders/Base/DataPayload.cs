using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Base
{
    public class DataPayload<T> : Payload<T> where T : class, new()
    {
        public T Entity { get; set; }
    }

    public class QueryPayload<T> : Payload<T> where T : class, new()
    {
        public bool QueryAll { get; set; }
    }

    public class Payload<T> where T : class, new()
    {
        public IdentifierBase Identifier { get; set; }
        public string DbName { get; set; }
        public string TableName { get; set; }
        public string UniqueIdFieldName { get; set; }
    }
}
