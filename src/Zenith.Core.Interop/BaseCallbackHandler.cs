using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Interop.Message;

namespace Zenith.Core.Interop
{
    public interface IJsonCallbackHandler
    {
        void OnReceiveJson(string json);
    }

    public abstract class JsonCallbackHandler<T> : IJsonCallbackHandler
    {
        protected abstract void MessageReceived(T obj);

        protected virtual T Deserialize(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        public void OnReceiveJson(string json)
        {
            MessageReceived(Deserialize(json));
        }
    }
}
