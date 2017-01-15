using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Interop;
using Zenith.Core.Interop.Message;
using Zenith.Core.Runtime.Processing;
using Zenith.Core.Shared.EventAggregation;

namespace Zenith.Core.TestConsole
{
    public class Program
    {
        public static void Main()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            TestInput input = new TestInput();
            input.Name = "Test";
            input.Number = 12;
            //input.Time = DateTime.Now;

            //IBridgeCallback callback = new TestProcessingCallback();
            IBridgeCallback callback = new DefaultProcessingCallback();
            LocalRequest request = new LocalRequest(callback);

            for (int i = 0; i < 10; i++)
            {
                request.Send(new ProcessingInput() { JsonData = serializer.Serialize(input), RequestId = DateTime.Now.Ticks.ToString("x") });
            }
        }

        class TestProcessingCallback : BridgeCallback
        {
            public TestProcessingCallback()
                : base("tcp://localhost:18800", EventAggregator.Instance)
            {
                base.AddJsonHandler<TestOutput>(new TestCallbackHandler());
                base.AddJsonHandler<TestOutput2>(new TestCallbackHandler2());
            }
        }

        class TestCallbackHandler : JsonCallbackHandler<TestOutput>
        {
            protected override void MessageReceived(TestOutput obj)
            {
                Console.WriteLine(obj);
            }
        }

        class TestCallbackHandler2 : JsonCallbackHandler<TestOutput2>
        {
            protected override void MessageReceived(TestOutput2 obj)
            {
                Console.WriteLine(obj);
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
