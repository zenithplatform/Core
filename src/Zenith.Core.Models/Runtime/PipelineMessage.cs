﻿using Newtonsoft.Json;
using Zenith.Core.Shared.Extensions;
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

        [JsonProperty("request_id")]
        public string RequestId
        {
            get;
            set;
        }

        [JsonProperty("execution_info")]
        public IDictionary<string, string> ExecutionInfo
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("{0} -> (Token : {1}, RequestId : {2}, ExecutionInfo : {3})", this.GetType().Name, Token, RequestId, ExecutionInfo.Dump());
        }
    }
}
