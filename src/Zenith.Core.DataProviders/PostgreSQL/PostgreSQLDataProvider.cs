using System;
using Npgsql;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.Enums;
using Zenith.Core.DataProviders.Interfaces;

namespace Zenith.Core.DataProviders.PostgreSQL
{
    public class PostgreSQLDataProvider : BaseDataProvider
    {
        NpgsqlConnection _connection = null;
        IConnectionFactory _connectionFactory = null;
        ConnectionParams _connectionParams = null;

        public PostgreSQLDataProvider(IConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
                _connectionFactory = new DefaultConnectionFactory();
            else
                _connectionFactory = connectionFactory;
        }

        public override void Open()
        {
            _connection = _connectionFactory.CreateConnection(ConnectionType.PostgreSQL, _connectionParams) as NpgsqlConnection;
            _connection.Open();
        }

        public override void Close()
        {
            _connection.Close();
        }

        protected override DataStorageResult Insert<T>(DataPayload<T> insertPayload)
        {
            return new DataStorageResult();
        }

        protected override DataStorageResult Update<T>(DataPayload<T> updatePayload)
        {
            return new DataStorageResult();
        }

        protected override DataStorageResult Delete<T>(DataPayload<T> deletePayload)
        {
            return new DataStorageResult();
        }

        protected override DataStorageResult<T> Query<T>(QueryPayload<T> queryPayload)
        {
            return new DataStorageResult<T>();
        }

        public override void PreExecuteOperation<T>(Operation operation, Payload<T> payload)
        {
            base.PreExecuteOperation<T>(operation, payload);
        }

        public override void PostExecuteOperation<T>(Operation operation, Payload<T> payload)
        {
            base.PostExecuteOperation<T>(operation, payload);
        }

        public override bool IsOpen
        {
            get
            {
                return (_connection.State == System.Data.ConnectionState.Open);
            }
        }

        protected override ConnectionParams ConnectionParams
        {
            get
            {
                return _connectionParams;
            }
            set
            {
                _connectionParams = value;
            }
        }

        /*

        public DataTable GetDataRawQuery(string queryText)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();

            try
            {
                using (NpgsqlTransaction tran = _connection.BeginTransaction())
                {
                    ds.Reset();

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(queryText, _connection))
                    {
                        adapter.Fill(ds);
                        table = ds.Tables[0];
                    }

                    tran.Commit();
                }
            }
            catch (NpgsqlException ex)
            {
                return null;
            }
            catch (Exception exc)
            {
                return null;
            }

            return table;
        }

        public DataTable GetDataFunction(string functionName)
        {
            DataTable dtRecord = new DataTable();

            try
            {
                using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(functionName, _connection))
                {
                    using (NpgsqlTransaction tran = _connection.BeginTransaction())
                    {
                        pgsqlcommand.CommandType = CommandType.StoredProcedure;

                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(pgsqlcommand))
                        {
                            adapter.Fill(dtRecord);
                        }

                        tran.Commit();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return dtRecord;
        }

        */
    }
}
