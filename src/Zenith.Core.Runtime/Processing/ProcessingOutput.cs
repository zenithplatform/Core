using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop.Message;

namespace Zenith.Core.Runtime.Processing
{
    public class ProcessingOutput
    {
        public string processor { get; set; }
        public string result { get; set; }

        public override string ToString()
        {
            return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, processor, result);
        }
    }
}
