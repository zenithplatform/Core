using JsonApiNet;
using JsonApiNet.Components;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.VirtualObservatory.Objects;

namespace Zenith.Core.Models.Serialization
{
    public static class JsonApiHelper
    {
        public static T Deserialize<T>(string json) where T : class
        {
            return JsonApi.ResourceFromDocument<T>(json);
        }

        public static JsonApiDocument DeserializeToDocument<T>(string json) where T : class
        {
            return JsonApi.Document<T>(json);
        }
    }

    public class InheritanceSerializationBinder : DefaultSerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            switch (typeName)
            {
                //case "ValueBase": return typeof(ValueBase);
                case "ComplexValue": return typeof(ComplexValue);
                case "ComplexItem": return typeof(ComplexItem);
                case "CompositeItem": return typeof(CompositeItem);
                case "MultiItem": return typeof(MultiItem);
                default: return base.BindToType(assemblyName, typeName);
            }
        }
    }

}
