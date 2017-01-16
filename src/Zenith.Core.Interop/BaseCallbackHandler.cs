using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Shared.Serialization;

namespace Zenith.Core.Interop
{
    public interface IJsonCallbackHandler
    {
        void OnReceiveJson(string json);
    }

    public abstract class JsonCallbackHandler<T> : IJsonCallbackHandler where T : class
    {
        protected abstract void MessageReceived(T obj);

        protected virtual T Deserialize(string json)
        {
            return SerializationHelper.Deserialize<T>(json);
        }

        public void OnReceiveJson(string json)
        {
            MessageReceived(Deserialize(json));
        }
    }
}
