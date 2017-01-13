using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models
{
    public class Catalog
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("is_obsolete")]
        public bool IsObsolete { get; set; }
        [JsonProperty("density")]
        public double Density { get; set; }
        [JsonProperty("popularity")]
        public double Popularity { get; set; }
        [JsonProperty("media")]
        public string Media { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
        [JsonProperty("mission")]
        public string Mission { get; set; }
        [JsonProperty("wavelengths")]
        public List<string> Wavelengths { get; set; }
        [JsonProperty("astronomy")]
        public List<string> Astronomy { get; set; }
        [JsonProperty("links")]
        public List<string> Links { get; set; }
    }

    public class KnownCatalog
    {
        [JsonProperty("short_code")]
        public string ShortCode { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
