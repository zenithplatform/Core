using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Shared.Configuration;

namespace Zenith.Core.DataProviders
{
    public class ConnectionParams
    {
        public string Server { get; set; }
        public string Url { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool OpenImmidiately { get; set; }
        public string ConnectionString { get; set; }
        public int Timeout { get; set; }

        public static ConnectionParams FromConfig()
        {
            return FromConfig("ConnectionParams");
        }

        public static ConnectionParams FromConfig(string sectionName)
        {
            return ConfigHelper.ObjectFromConfig<ConnectionParams>(sectionName);
        }
    }
}
