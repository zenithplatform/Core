using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.Interfaces;

namespace Zenith.Core.DataProviders.SQLServer
{
    public class SQLServerDataProvider : BaseDataProvider
    {
        GenericDbContext _dbContext = null;
        ConnectionParams _connectionParams = null;
        IConnectionFactory _connectionFactory = null;

        public SQLServerDataProvider(IConnectionFactory connectionFactory)//, ConnectionParams connectionParams)
        {
            if (connectionFactory == null)
                _connectionFactory = new DefaultConnectionFactory();
            else
                _connectionFactory = connectionFactory;
            
        }

        //public SQLServerDataProvider(ConnectionParams connectionParams)
        //    : this(null, connectionParams)
        //{

        //}

        public override bool IsOpen
        {
            get
            {
                return _dbContext.IsAvailable;
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
            _dbContext.CleanUp();
        }

        protected override DataStorageResult Delete<T>(DataPayload<T> deletePayload)
        {
            throw new NotImplementedException();
        }

        protected override DataStorageResult Insert<T>(DataPayload<T> insertPayload)
        {
            DbSet set = _dbContext.Set<T>();

            return null;
            //object result = _dbContext.Set(typeof(T)).Add(insertPayload.Entity);
            //_dbContext.SaveChanges();

            //return new DataResult() { Context = result, ErrorInfo = null, Success = true };
        }

        public override void Open()
        {
            _dbContext = _connectionFactory.CreateConnection(ConnectionType.SQLServer, _connectionParams) as GenericDbContext;
            _dbContext.OpenConnection();
        }

        protected override DataStorageResult<T> Query<T>(QueryPayload<T> queryPayload)
        {
            //object result = _dbContext.Set(typeof(T)).Find(new object[] { queryPayload.Identifier.Value });

            //return new DataStorageResult() { Context = result, ErrorInfo = null, Success = true };
            return null;
        }

        protected override DataStorageResult Update<T>(DataPayload<T> updatePayload)
        {
            throw new NotImplementedException();
        }
    }
}
