using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.VirtualObservatory
{
    public class VirtualObservatoryObjects<T> : VirtualObservatoryResponseBase where T : class
    {
        [JsonProperty("objects")]
        public List<T> Objects { get; set; }
    }

    public class VirtualObservatoryCatalogs<T> : VirtualObservatoryResponseBase where T : class
    {
        [JsonProperty("catalogs")]
        public List<T> Catalogs { get; set; }
    }

    public class VirtualObservatoryKnownCatalogs<T> : VirtualObservatoryResponseBase where T : class
    {
        [JsonProperty("known_catalogs")]
        public List<T> Catalogs { get; set; }
    }

    public abstract class VirtualObservatoryResponseBase
    {

    }
}
