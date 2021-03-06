﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.VirtualObservatory.Objects
{
    public class CelestialObject
    {
        List<DataItemBase> _objectData = null;

        public CelestialObject()
        {
            _objectData = new List<DataItemBase>();
        }

        [JsonProperty("object_data")]
        public List<DataItemBase> ObjectData
        {
            get { return _objectData; }
            set { _objectData = value; }
        }
    }

    public class ValueBase
    {
        [JsonProperty("value")]
        public object Value { get; set; }
    }

    public class ComplexValue : ValueBase
    {
        [JsonProperty("unit")]
        public string Unit { get; set; }
        [JsonProperty("error")]
        public object Error { get; set; }

        public override string ToString()
        {
            string ret = "";

            ret += " " + this.Value + this.Unit;

            return ret;
        }
    }

    public class DataItemBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("kind")]
        public string Kind { get; set; }
        [JsonProperty("values")]
        public List<ValueBase> Values { get; set; }
    }

    public class SimpleItem : DataItemBase
    {

    }

    public class ComplexItem : DataItemBase
    {

    }

    public class CompositeItem : DataItemBase
    {

    }

    public class MultiItem : DataItemBase
    {

    }

    public class MultiComplexItem : DataItemBase
    {

    }
}
