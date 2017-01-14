using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Metadata
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        string _tableName = "";

        public TableNameAttribute(string tableName)
        {
            _tableName = tableName;
        }

        public string TableName { get { return _tableName; } }
    }
}
