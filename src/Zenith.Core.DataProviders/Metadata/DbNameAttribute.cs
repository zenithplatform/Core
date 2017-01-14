using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Metadata
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbNameAttribute : Attribute
    {
        string _dbName = "";

        public DbNameAttribute(string dbName)
        {
            this._dbName = dbName;
        }

        public string DbName { get { return _dbName; } }
    }
}
