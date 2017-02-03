using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Runtime.Infrastructure
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
                return json;
                //JObject parsed = JObject.Parse(json);

                //return parsed["json_data"].ToString();
            }
            catch (Exception exc)
            {
                return string.Empty;
            }
        }
    }

    //public interface IJsonMessagePreProcessor
    //{
    //    T PreProcess<T>(string json) where T : class;
    //}

    //public class DefaultJsonMessagePreProcessor : IJsonMessagePreProcessor
    //{
    //    //public string PreProcess(string json)
    //    //{
    //    //    try
    //    //    {
    //    //        JObject parsed = JObject.Parse(json);

    //    //        return parsed["body"].ToString();
    //    //    }
    //    //    catch(Exception exc)
    //    //    {
    //    //        return string.Empty;
    //    //    }
    //    //}

    //    public T PreProcess<T>(string json) where T : class
    //    {
    //        try
    //        {
    //            JObject parsed = JObject.Parse(json);
    //            Metadata metadata = SerializationHelper.Deserialize<Metadata>(parsed["metadata"].ToString());

    //            return parsed["body"].ToString();
    //        }
    //        catch (Exception exc)
    //        {
    //            return string.Empty;
    //        }
    //    }
    //}
}
