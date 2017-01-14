using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.Interfaces;

namespace Zenith.Core.DataProviders.Rethink
{
    public class RethinkDBDataProvider : BaseDataProvider
    {
        private static RethinkDB R = RethinkDB.R;
        IConnectionFactory _connectionFactory = null;
        ConnectionParams _connectionParams = null;
        Connection _conn = null;

        public RethinkDBDataProvider(IConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
                connectionFactory = new DefaultConnectionFactory();

            _connectionFactory = connectionFactory;
        }

        public override bool IsOpen
        {
            get
            {
                return (_conn != null) && _conn.Open;
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

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            _conn = _connectionFactory.CreateConnection(ConnectionType.RethinkDB, _connectionParams) as Connection;
        }

        protected override DataStorageResult Delete<T>(DataPayload<T> deletePayload)
        {
            DataStorageResult storageResult = new DataStorageResult();

            try
            {
                var result = R.Db(deletePayload.DbName)
                              .Table(deletePayload.TableName)
                              .Get(deletePayload.Identifier.Value)
                              .Delete()
                              .Run(_conn);

                //var result = R.Db(deletePayload.DbName)
                //              .Table(deletePayload.TableName)
                //              .Filter(deletePayload.Identifier.Value)
                //              .Delete()
                //              .Run(_conn);

                storageResult.Success = true;
            }
            catch (Exception exc)
            {
                storageResult.Success = false;
                storageResult.Error = exc;
            }

            return storageResult;
        }

        protected override DataStorageResult Insert<T>(DataPayload<T> insertPayload)
        {
            DataStorageResult storageResult = new DataStorageResult();

            try
            {
                var result = R.Db(insertPayload.DbName)
                              .Table(insertPayload.TableName)
                              .Insert(insertPayload.Entity)
                              .Run(_conn);

                storageResult.Success = true;
            }
            catch(Exception exc)
            {
                storageResult.Success = false;
                storageResult.Error = exc;
            }

            return storageResult;
        }

        protected override DataStorageResult<T> Query<T>(QueryPayload<T> queryPayload)
        {
            DataStorageResult<T> storageResult = new DataStorageResult<T>();

            try
            {
                var result = R.Db(queryPayload.DbName)
                              .Table(queryPayload.TableName)
                              .GetAll(queryPayload.Identifier.Value)
                              .Run(_conn);

                storageResult.Success = true;
                storageResult.Context = result as T;
            }
            catch (Exception exc)
            {
                storageResult.Success = false;
                storageResult.Error = exc;
            }

            return storageResult;
        }

        protected override DataStorageResult Update<T>(DataPayload<T> updatePayload)
        {
            DataStorageResult storageResult = new DataStorageResult();

            try
            {
                var result = R.Db(updatePayload.DbName)
                              .Table(updatePayload.TableName)
                              .Update(updatePayload.Entity)
                              .Run(_conn);

                storageResult.Success = true;
            }
            catch (Exception exc)
            {
                storageResult.Success = false;
                storageResult.Error = exc;
            }

            return storageResult;
        }
    }
}
