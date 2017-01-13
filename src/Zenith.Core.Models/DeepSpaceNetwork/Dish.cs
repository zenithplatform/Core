using System;
using System.Collections.Generic;

namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class Dish
    {
        public string Identifier;
        public string Name;
        public string Type;
        public MeasuredValue<decimal> AzimuthAngle;
        public MeasuredValue<decimal> ElevationAngle;
        public MeasuredValue<decimal> WindSpeed;
        /// <summary>
        /// Multiple Spacecraft Per Aperture
        /// </summary>
        public bool IsMSPA;
        public bool IsArray;
        /// <summary>
        /// Delta-Differenced One-Way Range
        /// </summary>
        public bool IsDDOR;
        public DateTime Created;
        public DateTime Updated;

        public List<Spacecraft> Targets;
        public List<Signal> Signals;
    }
}
