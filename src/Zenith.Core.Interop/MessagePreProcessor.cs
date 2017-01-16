using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Interop
{
    public interface IJsonMessagePreProcessor
    {
        string PreProcess(string json);
    }

    public class DefaultJsonMessagePreProcessor : IJsonMessagePreProcessor
    {
        public string PreProcess(string json)
        {
            try
            {
                JObject parsed = JObject.Parse(json);
                return parsed["json_data"].ToString();
            }
            catch(Exception exc)
            {
                return string.Empty;
            }
        }
    }
}
