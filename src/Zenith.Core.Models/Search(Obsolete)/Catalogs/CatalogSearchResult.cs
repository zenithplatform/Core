using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Interfaces;

namespace Zenith.Core.Models
{
    public class CatalogSearchResult:ICatalogSearchResult<Catalog>
    {
        private List<Catalog> _catalogs = null;

        public CatalogSearchResult()
        {

        }

        [JsonProperty("catalogs")]
        public List<Catalog> Catalogs
        {
            get
            {
                return _catalogs;
            }
            set
            {
                _catalogs = value;
            }
        }
    }
}
