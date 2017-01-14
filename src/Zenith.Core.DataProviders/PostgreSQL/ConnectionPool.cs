using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.PostgreSQL
{
    public static class ConnectionPool
    {
        static List<NpgsqlConnection> _allConnections = new List<NpgsqlConnection>();

        public static bool CreateConnection(ConnectionParams connectionParams, out NpgsqlConnection createdConnection)
        {
            bool result = false;
            string connstring = "";
            createdConnection = null;

            try
            {
                connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};",
                                               connectionParams.Server, connectionParams.Port, connectionParams.Username, connectionParams.Password, connectionParams.Database);

                createdConnection = new NpgsqlConnection(connstring);

                if(connectionParams.OpenImmidiately)
                    createdConnection.Open();

                _allConnections.Add(createdConnection);
            }
            catch (Exception exc)
            {
                if (createdConnection != null && createdConnection.State == System.Data.ConnectionState.Open)
                    createdConnection.Close();

                createdConnection = null;
            }

            return result;
        }
    }
}
