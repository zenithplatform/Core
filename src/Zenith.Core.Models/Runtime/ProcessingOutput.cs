using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.Runtime
{
    public class ProcessingOutput
    {
        [JsonProperty("body")]
        public OutputBody Body { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Body : {1}, Metadata : {2}", this.GetType().Name, Body, Metadata);
            //return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, Processor, Result);
        }
    }

    public class OutputBody
    {
        [JsonProperty("processor")]
        public string Processor { get; set; }
        [JsonProperty("result")]
        public string RawResult { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
    }

    //public class ProcessingOutput
    //{
    //    [JsonProperty("processor")]
    //    public string Processor { get; set; }
    //    [JsonProperty("result")]
    //    public string Result { get; set; }

    //    public override string ToString()
    //    {
    //        return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, Processor, Result);
    //    }
    //}
}
