using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models
{
    public class ObservationValuePrimitive
    {
        public ValueSign SignSymbol { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public decimal Correction { get; set; }
    }

    public class ObservationValue
    {
        List<ObservationValuePrimitive> _primitives = new List<ObservationValuePrimitive>();

        public List<ObservationValuePrimitive> ValueParts
        {
            get { return _primitives; }
        }

        public void AddPrimitive(string value)
        {
            _primitives.Add(new ObservationValuePrimitive() { Value = value, Unit = "", Correction = 0 });
        }

        public void AddPrimitive(string value, string unit)
        {
            _primitives.Add(new ObservationValuePrimitive() { Value = value, Unit = unit, Correction = 0 });
        }

        public void AddPrimitive(string value, string unit, decimal correction)
        {
            _primitives.Add(new ObservationValuePrimitive() { Value = value, Unit = unit, Correction = correction });
        }

        public override string ToString()
        {
            string ret = "";

            if (ValueParts != null && ValueParts.Count > 0)
            {
                foreach (ObservationValuePrimitive val in ValueParts)
                {
                    ret += " " + val.Value + val.Unit;
                }
            }

            return ret;
        }
    }

    public enum ValueSign
    {
        Plus,
        Minus,
        PlusMinus,
        Approximately,
        None
    }

    public static class ValueSignExtensions
    {
        public static string SignToString(this ValueSign sign)
        {
            string ret = "";

            switch(sign)
            {
                case ValueSign.Plus:
                    ret = "+";
                    break;
                case ValueSign.Minus:
                    ret = "-";
                    break;
                case ValueSign.PlusMinus:
                    ret = ((char)0x00B1).ToString();
                    break;
                case ValueSign.Approximately:
                    ret = ((char)0x223C).ToString();
                    break;
            }

            return ret;
        }
    }
}
