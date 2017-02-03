using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Zenith.Core.Models.Runtime;
using Zenith.Core.Runtime.Infrastructure;
using Zenith.Core.Runtime.Processing;

namespace Zenith.Core.TestConsole
{
    public class Program
    {
        public static void Main()
        {
            IPipeline pipeline = new Pipeline("tcp://localhost:18801");
            IPipelineCallback callback = pipeline.CreateCallback<DefaultProcessingCallback>();
            callback.Activate();

            while (true)
            {
                Console.WriteLine("Ready");
                Console.ReadLine();
                Console.WriteLine("Running");
                try
                {
                    //TestInput input = new TestInput();
                    //input.Name = "Test";
                    //input.Number = 12;

                    //ProcessingRequest request = new ProcessingRequest(bridge);
                    //request.Send(new ProcessingInput() { JsonData = SerializationHelper.Serialize(input), RequestId = DateTime.Now.Ticks.ToString("x") });

                    //TestInput obj = new TestInput();
                    //obj.Name = "Test";
                    //obj.Number = 12;

                    //ProcessingRequestHandler requestHandler = new ProcessingRequestHandler(pipeline);
                    //Metadata metadata = new Metadata() { RequestId = DateTime.Now.Ticks.ToString("x"), Token = DateTime.Now.Ticks.ToString("x") };

                    //PipelineMessage<TestInput> payload = new PipelineMessage<TestInput>();
                    //payload.Body = obj;
                    //payload.Metadata = metadata;

                    //requestHandler.Send(payload);


                    Thread first = new Thread(Run);
                    first.Name = "First thread";
                    Thread second = new Thread(Run);
                    second.Name = "Second thread";

                    first.Start(new ThreadData() { Pipeline = pipeline });
                    second.Start(new ThreadData() { Pipeline = pipeline });
                }
                catch (Exception exc)
                {

                }
            }
        }

        internal class ThreadData
        {
            internal IPipeline Pipeline { get; set; }
        }

        private static void Run(object data)
        {
            IPipeline pipeline = ((ThreadData)data).Pipeline;

            for(int i = 0; i < 10; i++)
            {
                TestInput obj = new TestInput();
                obj.Name = "Test";
                obj.Number = 12;

                ProcessingRequestHandler requestHandler = new ProcessingRequestHandler(pipeline);
                Metadata metadata = new Metadata() { RequestId = DateTime.Now.Ticks.ToString("x"), Token = DateTime.Now.Ticks.ToString("x") };

                PipelineMessage<TestInput> payload = new PipelineMessage<TestInput>();
                payload.Body = obj;
                payload.Metadata = metadata;

                requestHandler.Send(payload);
                Thread.Sleep(new Random().Next(100, 800));
            }
        }

        //class TestProcessingCallback : BridgeCallback
        //{
        //    public TestProcessingCallback()
        //        : base("tcp://localhost:18800")
        //    {
        //        base.AddJsonHandler<TestOutput>(new TestCallbackHandler());
        //        base.AddJsonHandler<TestOutput2>(new TestCallbackHandler2());
        //    }
        //}

        //class TestCallbackHandler : JsonCallbackHandler<TestOutput>
        //{
        //    protected override void MessageReceived(TestOutput obj)
        //    {
        //        Console.WriteLine(string.Format("Thread : {0}, data : {1} ", Thread.CurrentThread.Name, obj));
        //    }
        //}

        //class TestCallbackHandler2 : JsonCallbackHandler<TestOutput2>
        //{
        //    protected override void MessageReceived(TestOutput2 obj)
        //    {
        //        Console.WriteLine(string.Format("Thread : {0}, data : {1} ", Thread.CurrentThread.Name, obj));
        //    }
        //}
        [JsonObject("test_input")]
        public class TestInput
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            //public DateTime Time { get; set; }
            [JsonProperty("number")]
            public int Number { get; set; }
        }

        //class TestOutput : CallbackMessageBase
        //{
        //    public string processor { get; set; }
        //    public string result { get; set; }

        //    public override string ToString()
        //    {
        //        return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, processor, result);
        //    }
        //}

        //class TestOutput2 : CallbackMessageBase
        //{
        //    public string processor { get; set; }
        //    public string result { get; set; }

        //    public override string ToString()
        //    {
        //        return string.Format("{0} Processor : {1}, Result : {2}", this.GetType().Name, processor, result);
        //    }
        //}
    }
}
