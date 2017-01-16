using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared.Serialization
{
    public static class SerializationHelper
    {
        public static T Deserialize<T>(string json) where T : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static string Serialize<T>(T instance) where T : class
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
