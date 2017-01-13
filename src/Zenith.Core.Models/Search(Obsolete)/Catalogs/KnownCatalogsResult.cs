using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Interfaces;

namespace Zenith.Core.Models
{
    public class KnownCatalogsResult : ICatalogSearchResult<KnownCatalog>
    {
        private List<KnownCatalog> _catalogs = null;

        public KnownCatalogsResult()
        {

        }

        [JsonProperty("known_catalogs")]
        public List<KnownCatalog> Catalogs
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
