using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Shared;

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
            return string.Format("{0}\n {1}\n {2}\n", this.GetType().Name, Body, Metadata);
        }
    }

    public class OutputBody
    {
        [JsonProperty("result")]
        public string RawResult { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
        [JsonProperty("error_info")]
        public string ErrorInfo { get; set; }

        public override string ToString()
        {
            var plainError = string.Empty;

            if (Utils.IsBase64(ErrorInfo))
                plainError = Encoding.UTF8.GetString(Convert.FromBase64String(ErrorInfo));
            else
                plainError = ErrorInfo;

            return string.Format("{0} -> (RawResult : {1}, Status : {2}, StatusCode : {3}, ErrorInfo : {4})", this.GetType().Name, RawResult, Status, StatusCode, plainError);
        }
    }
}
