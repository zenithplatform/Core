using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.Runtime
{
    public class PipelineMessage<T>
    {
        [JsonProperty("metadata")]
        public Metadata Metadata
        {
            get;
            set;
        }

        [JsonProperty("body")]
        public T Body
        {
            get;
            set;
        }
    }

    public class Metadata
    {
        [JsonProperty("token")]
        public string Token
        {
            get;
            set;
        }

        [JsonProperty("req_id")]
        public string RequestId
        {
            get;
            set;
        }

        [JsonProperty("processor")]
        public string Processor
        {
            get;
            set;
        }
    }
}
