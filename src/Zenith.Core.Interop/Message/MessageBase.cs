using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Interop.Message
{
    public class MessageBase
    {
        public string JsonData
        {
            get;
            set;
        }

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
