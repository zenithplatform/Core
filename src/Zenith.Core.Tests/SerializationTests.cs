using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Zenith.Core.Models;
using Zenith.Core.Models.Serialization;
using Zenith.Core.Models.VirtualObservatory.Objects;

namespace Zenith.Core.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void ParseInputMockData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Binder = new InheritanceSerializationBinder()
            };

            string content = File.ReadAllText("simple_json4.txt");
            ObjectSearchResult items = JsonConvert.DeserializeObject<ObjectSearchResult>(content, settings);
        }

        [TestMethod]
        public void Serialize()
        {
            JavaScriptSerializer oJS = new JavaScriptSerializer();
            ObjectSearchResult result = new ObjectSearchResult();

            //List<ValueBase> vals = new List<ValueBase>();
            //vals.Add(new ValueBase() { Value = "TestSimpleVal1" });

            //result.Items.Add(new SimpleItem() { Name = "TestSimple1", Values = vals });

            //List<ValueBase> complex = new List<ValueBase>();
            //complex.Add(new ComplexValue() { Value = "TestComplex1", Unit = "TestUnit1", Error = "+2" });

            //result.Items.Add(new ComplexItem() { Name = "TestComplex1", Values = complex });

            //List<ValueBase> composite = new List<ValueBase>();
            //composite.Add(new ComplexValue() { Value = "TestComplex1", Unit = "TestUnit1", Error = "+2" });
            //composite.Add(new ComplexValue() { Value = "TestComplex1", Unit = "TestUnit1", Error = "+2" });

            //result.Items.Add(new CompositeItem() { Name = "TestComposite1", Values = composite });

            //List<ValueBase> multi = new List<ValueBase>();
            //multi.Add(new ValueBase() { Value = "TestMulti1" });
            //multi.Add(new ValueBase() { Value = "TestMulti2" });

            //result.Items.Add(new MultiItem() { Name = "TestMulti1", Values = multi });

            string json = oJS.Serialize(result);
        }

        [TestMethod]
        public void LoadObjectTypes()
        {
            string json = File.ReadAllText("sample_json6.txt");
            ObjectTypesContainer container = JsonConvert.DeserializeObject<ObjectTypesContainer>(json);
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml);
            //string jsonText = JsonConvert.SerializeXmlNode(doc);

            // To convert JSON text contained in string json into an XML node
            //XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
        }
    }
}
