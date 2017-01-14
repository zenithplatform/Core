using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Interfaces
{
    public interface IConnectionFactory
    {
        object CreateConnection(ConnectionType type, ConnectionParams connectionParams);
    }

    public enum ConnectionType
    {
        PostgreSQL,
        RavenBD,
        SQLServer,
        RethinkDB
    }
}
