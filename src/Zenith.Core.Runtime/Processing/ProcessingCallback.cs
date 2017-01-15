﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Interop;
using Zenith.Core.Shared.EventAggregation;

namespace Zenith.Core.Runtime.Processing
{
    public class DefaultProcessingCallback : BridgeCallback
    {
        public DefaultProcessingCallback()
            : base("tcp://localhost:18800", EventAggregator.Instance)
        {
            base.AddJsonHandler<ProcessingOutput>(new TracingCallbackHandler());
        }
    }
}