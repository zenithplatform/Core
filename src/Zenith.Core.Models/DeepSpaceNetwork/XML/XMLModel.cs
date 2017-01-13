using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Zenith.Core.Models.DeepSpaceNetwork.XML
{
    [XmlRoot("dsn")]
    public class DSNQueryResult
    {
        [XmlElement("station")]
        public List<DSNStation> Stations;

        [XmlElement("dish")]
        public List<DSNXMLDish> Dishes;

        [XmlElement("timestamp")]
        public string Timestamp;
    }

    [XmlRoot("config")]
    public class DSNConfigResult
    {
        [XmlElement("sites")]
        public DSNSites Sites;

        [XmlElement("spacecraftMap")]
        public DSNSpacecraftMap SpacecraftMap;
    }

    public class DSNSites
    {
        [XmlElement("site")]
        public List<DSNSite> Sites;
    }

    public class DSNSite
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("friendlyName")]
        public string FriendlyName;

        [XmlAttribute("flag")]
        public string Flag;

        [XmlElement("dish")]
        public List<DSNXMLDish> Dishes;
    }

    public class DSNSpacecraftMap
    {
        [XmlElement("spacecraft")]
        public List<DSNSpacecraft> Spacecrafts;
    }

    public class DSNSpacecraft
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("explorerName")]
        public string ExplorerName;

        [XmlAttribute("friendlyName")]
        public string FriendlyName;

        [XmlAttribute("thumbnail")]
        public string Thumbnail;
    }

    public class DSNStation
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("friendlyName")]
        public string FriendlyName;

        [XmlAttribute("timeUTC")]
        public string TimeUTC; // TODO convert to useful format

        [XmlAttribute("timeZoneOffset")]
        public string TimeZoneOffset; // TODO convert to useful format
    }

    public class DSNXMLDish
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("azimuthAngle")]
        public string AzimuthAngle;

        [XmlAttribute("elevationAngle")]
        public string ElevationAngle;

        [XmlAttribute("windSpeed")]
        public string WindSpeed;

        /// <summary>
        /// Multiple Spacecraft Per Aperture
        /// </summary>
        [XmlAttribute("isMSPA")]
        public string IsMSPA;

        [XmlAttribute("isArray")]
        public string IsArray;

        /// <summary>
        /// Delta-Differenced One-Way Range
        /// </summary>
        [XmlAttribute("isDDOR")]
        public string IsDDOR;

        [XmlAttribute("created")]
        public string Created;

        [XmlAttribute("updated")]
        public string Updated;

        [XmlElement("downSignal")]
        public List<DSNDownSignal> DownSignals;

        [XmlElement("upSignal")]
        public List<DSNUpSignal> UpSignals;

        [XmlElement("target")]
        public List<DSNTarget> Targets;

        // For Config
        [XmlAttribute("friendlyName")]
        public string FriendlyName;

        [XmlAttribute("type")]
        public string DishType;
    }

    public class DSNSignal
    {
        [XmlAttribute("signalType")]
        public string SignalType;

        [XmlAttribute("signalTypeDebug")]
        public string SignalTypeDebug;

        [XmlAttribute("dataRate")]
        public string DataRate;

        [XmlAttribute("frequency")]
        public string Frequency;

        [XmlAttribute("power")]
        public string Power;

        [XmlAttribute("spacecraft")]
        public string Spacecraft;

        [XmlAttribute("spacecraftId")]
        public string SpacecraftID;
    }

    public class DSNDownSignal : DSNSignal
    {
        public string Direction = "Down";
    }

    public class DSNUpSignal : DSNSignal
    {
        public string Direction = "Up";
    }

    public class DSNTarget
    {
        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("id")]
        public string ID;

        [XmlAttribute("uplegRange")]
        public string UplegRange;

        [XmlAttribute("downlegRange")]
        public string DownlegRange;

        [XmlAttribute("rtlt")]
        public string RoundTripLightTime;
    }
}
