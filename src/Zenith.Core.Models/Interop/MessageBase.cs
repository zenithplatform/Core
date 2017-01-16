using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.Interop
{
    public class MessageBase
    {
        [JsonProperty("json_data")]
        public string JsonData
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
    }

    public class CallbackMessageBase : MessageBase
    {

    }
}
