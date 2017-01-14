using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Metadata
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UniqueIdAttribute : Attribute
    {
        string _uniqueId = "";

        public UniqueIdAttribute(string uniqueId)
        {
            _uniqueId = uniqueId;
        }

        public string UniqueIdName { get { return _uniqueId; } }
    }
}
