using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Shared.Serialization;

namespace Zenith.Core.Runtime.Infrastructure
{
    public interface IJsonCallbackHandler
    {
        void OnReceiveJson(string json);
    }

    public abstract class JsonCallbackHandler<T> : IJsonCallbackHandler where T : class
    {
        protected abstract void MessageReceived(T obj);
        protected abstract void OnError(Exception exc);

        protected virtual T Deserialize(string json)
        {
            return SerializationHelper.Deserialize<T>(json);
        }

        public void OnReceiveJson(string json)
        {
            T data = default(T);

            try
            {
                data = Deserialize(json);
                MessageReceived(data);
            }
            catch (Exception exc)
            {
                OnError(exc);
            }
        }
    }
}
