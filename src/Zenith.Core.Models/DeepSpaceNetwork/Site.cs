using System;

namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class Site
    {
        public string Identifier;
        public string Name;
        public Coordinates Coordinates;
        public DateTime TimeReportedUTC;
        public int TimezoneOffsetMinutes;
    }
}
