using Npgsql;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.DataProviders.Interfaces;
using Zenith.Core.DataProviders.PostgreSQL;
using Zenith.Core.DataProviders.RavenDB;
using Zenith.Core.DataProviders.SQLServer;

namespace Zenith.Core.DataProviders
{
    public class DefaultConnectionFactory : IConnectionFactory
    {
        public object CreateConnection(ConnectionType type, ConnectionParams connectionParams)
        {
            object resultConnection = null;

            switch (type)
            {
                case ConnectionType.PostgreSQL:
                    NpgsqlConnection connection;
                    ConnectionPool.CreateConnection(connectionParams, out connection);
                    resultConnection = connection;

                    break;
                case ConnectionType.RavenBD:
                    resultConnection = new StoreInstance(connectionParams);

                    break;
                case ConnectionType.SQLServer:
                    GenericDbContext context = new GenericDbContext(connectionParams);
                    resultConnection = context;
                    break;
                case ConnectionType.RethinkDB:
                    try
                    {
                        //RethinkDB R = RethinkDB.R;
                        //Connection _connection = R.Connection()
                        //               .Hostname("localhost")
                        //               .Port(RethinkDBConstants.DefaultPort + 1)
                        //               .Timeout(60)
                        //               .Connect();

                        RethinkDB R = RethinkDB.R;
                        Connection _connection = R.Connection()
                                                  .Hostname(connectionParams.Server)
                                                  .Port(connectionParams.Port)
                                                  .Timeout(connectionParams.Timeout)
                                                  .Connect();

                        resultConnection = _connection;
                    }
                    catch (Exception exc)
                    {
                        //throw;
                    }

                    break;
            }

            return resultConnection;
        }
    }
}