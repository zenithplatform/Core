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
        [JsonProperty("processor")]
        public string Processor { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, Processor, Result);
        }
    }
}
