using System;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.Enums;
using Zenith.Core.DataProviders.Interfaces;

namespace Zenith.Core.DataProviders
{
    public interface IDataProvider
    {
        void Open();
        void Close();
        bool IsOpen { get; }
        DataStorageResult Execute<T>(Operation operation, Payload<T> payload) where T : class, new();
        void PreExecuteOperation<T>(Operation operation, Payload<T> payload) where T : class, new();
        void PostExecuteOperation<T>(Operation operation, Payload<T> payload) where T : class, new();
    }

    public abstract class BaseDataProvider : IDataProvider// : IDataSourceOperations
    {
        public abstract void Open();
        public abstract void Close();

        public abstract bool IsOpen { get; }

        public DataStorageResult Execute<T>(Operation operation, Payload<T> payload) where T : class, new()
        {
            DataStorageResult result = null;

            PreExecuteOperation<T>(operation, payload);

            try
            {
                switch (operation)
                {
                    case Operation.Delete:
                        result = this.Delete<T>(payload as DataPayload<T>);
                        break;
                    case Operation.Insert:
                        result = this.Insert<T>(payload as DataPayload<T>);
                        break;
                    case Operation.Query:
                        result = this.Query<T>(payload as QueryPayload<T>) as DataStorageResult<T>;
                        break;
                    case Operation.Update:
                        result = this.Update<T>(payload as DataPayload<T>);
                        break;
                }
            }
            catch(Exception exc)
            {

            }
            finally
            {
                PostExecuteOperation<T>(operation, payload);
            }

            return result;
        }

        public virtual void PreExecuteOperation<T>(Operation operation, Payload<T> payload) where T : class, new()
        {

        }

        public virtual void PostExecuteOperation<T>(Operation operation, Payload<T> payload) where T : class, new()
        {

        }

        protected abstract DataStorageResult Insert<T>(DataPayload<T> insertPayload) where T : class, new();
        protected abstract DataStorageResult Update<T>(DataPayload<T> updatePayload) where T : class, new();
        protected abstract DataStorageResult Delete<T>(DataPayload<T> deletePayload) where T : class, new();
        protected abstract DataStorageResult<T> Query<T>(QueryPayload<T> queryPayload) where T : class, new();

        protected abstract ConnectionParams ConnectionParams { get; set; }
    }
}
