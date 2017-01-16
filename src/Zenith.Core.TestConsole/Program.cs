using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Interop;
using Zenith.Core.Models.Interop;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Runtime.Processing;
using Zenith.Core.Shared.EventAggregation;
using Zenith.Core.Shared.Serialization;

namespace Zenith.Core.TestConsole
{
    public class Program
    {
        public static void Main()
        {
            IPyBridge bridge = new PyBridge("tcp://localhost:18801");
            IBridgeCallback callback = bridge.CreateCallback<DefaultProcessingCallback>();
            callback.Activate();

            Thread first = new Thread(Run);
            first.Name = "First thread";
            Thread second = new Thread(Run);
            second.Name = "Second thread";

            first.Start(new ThreadData() { Bridge = bridge});
            second.Start(new ThreadData() { Bridge = bridge });
        }

        internal class ThreadData
        {
            internal IPyBridge Bridge { get; set; }
        }

        private static void Run(object data)
        {
            IPyBridge bridge = ((ThreadData)data).Bridge;

            TestInput input = new TestInput();
            input.Name = "Test";
            input.Number = 12;

            ProcessingRequest request = new ProcessingRequest(bridge);
            request.Send(new ProcessingInput() { JsonData = SerializationHelper.Serialize(input), RequestId = DateTime.Now.Ticks.ToString("x") });
        }

        class TestProcessingCallback : BridgeCallback
        {
            public TestProcessingCallback()
                : base("tcp://localhost:18800", null)
            {
                base.AddJsonHandler<TestOutput>(new TestCallbackHandler());
                base.AddJsonHandler<TestOutput2>(new TestCallbackHandler2());
            }
        }

        class TestCallbackHandler : JsonCallbackHandler<TestOutput>
        {
            protected override void MessageReceived(TestOutput obj)
            {
                Console.WriteLine(string.Format("Thread : {0}, data : {1} ", Thread.CurrentThread.Name, obj));
            }
        }

        class TestCallbackHandler2 : JsonCallbackHandler<TestOutput2>
        {
            protected override void MessageReceived(TestOutput2 obj)
            {
                Console.WriteLine(string.Format("Thread : {0}, data : {1} ", Thread.CurrentThread.Name, obj));
            }
        }

        public class TestInput
        {
            public string Name { get; set; }
            //public DateTime Time { get; set; }
            public int Number { get; set; }
        }

        class TestOutput : CallbackMessageBase
        {
            public string processor { get; set; }
            public string result { get; set; }

            public override string ToString()
            {
                return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, processor, result);
            }
        }

        class TestOutput2 : CallbackMessageBase
        {
            public string processor { get; set; }
            public string result { get; set; }

            public override string ToString()
            {
                return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, processor, result);
            }
        }
    }
}
