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

            IBridgeCallback callback = new TestProcessingCallback();
            LocalRequest request = new LocalRequest(callback);

            for (int i = 0; i < 10; i++)
            {
                request.Send(new ProcessingInput() { JsonData = serializer.Serialize(input), RequestId = DateTime.Now.Ticks.ToString("x") });
            }
        }

        class TestProcessingCallback : BridgeCallback<TestOutput>
        {
            public TestProcessingCallback()
                : base("tcp://localhost:18800", new TestCallbackHandler(), EventAggregator.Instance)
            {

            }
        }

        class TestCallbackHandler : IBaseCallbackHandler<TestOutput>
        {
            public void OnReceive(TestOutput data)
            {
                //Console.WriteLine(string.Format("Received {0}", data.GetType().Name));
                Console.WriteLine(data);
            }
        }

        public class TestInput
        {
            public string Name { get; set; }
            //public DateTime Time { get; set; }
            public int Number { get; set; }
        }

        class TestOutput : MessageBase
        {
            public string processor { get; set; }
            public string result { get; set; }

            public override string ToString()
            {
                return string.Format("Processor : {0}, Result : {1}", processor, result);
            }
        }
    }
}
