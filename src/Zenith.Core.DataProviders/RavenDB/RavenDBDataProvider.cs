using System;
using Raven.Client;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.Enums;
using Zenith.Core.DataProviders.Interfaces;

namespace Zenith.Core.DataProviders.RavenDB
{
    public class RavenDBDataProvider : BaseDataProvider
    {
        private StoreInstance _storeInstance;
        ConnectionParams _connectionParams = null;
        IConnectionFactory _connectionFactory = null;
        private IDocumentSession _documentSession;

        public RavenDBDataProvider(IConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
                _connectionFactory = new DefaultConnectionFactory();
            else
                _connectionFactory = connectionFactory;
        }

        public override void Open()
        {
            _storeInstance = _connectionFactory.CreateConnection(ConnectionType.RavenBD, _connectionParams) as StoreInstance;
            _documentSession = _storeInstance.Store.OpenSession();
        }

        public override void Close()
        {
            //_documentSession.SaveChanges();
            _documentSession.Dispose();
            _documentSession = null;
        }

        protected override DataStorageResult Insert<T>(DataPayload<T> insertPayload)
        {
            //_documentSession.Store(insertPayload.Entity);

            //DataStorageResult result = new DataStorageResult();
            //result.Context = null;
            //result.ErrorInfo = null;
            //result.Success = true;

            //return result;

            return null;
        }

        protected override DataStorageResult Update<T>(DataPayload<T> updatePayload)
        {
            //_documentSession.Store(updatePayload.Entity);

            //DataResult result = new DataResult();
            //result.Context = null;
            //result.ErrorInfo = null;
            //result.Success = true;

            //return result;

            return null;
        }

        protected override DataStorageResult Delete<T>(DataPayload<T> deletePayload)
        {
            //_documentSession.Delete(deletePayload.Entity);

            //DataResult result = new DataResult();
            //result.Context = null;
            //result.ErrorInfo = null;
            //result.Success = true;

            //return result;
            return null;
        }

        public override void PreExecuteOperation<T>(Operation operation, Payload<T> payload)
        {
            base.PreExecuteOperation<T>(operation, payload);
        }

        public override void PostExecuteOperation<T>(Operation operation, Payload<T> payload)
        {
            base.PostExecuteOperation<T>(operation, payload);
        }

        protected override DataStorageResult<T> Query<T>(QueryPayload<T> queryPayload)
        {
            //DataResult result = new DataResult();
            ////result.Context = _documentSession.Query<T>();
            //result.ErrorInfo = null;
            //result.Success = true;

            //return result;
            return null;
        }

        public override bool IsOpen
        {
            get
            {
                return _documentSession != null;
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
    }
}
