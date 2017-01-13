using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.Interfaces;

namespace Zenith.Core.Models
{
    public class CatalogSearchRequest:ISearchRequest
    {
        string _searchTerm = "";

        public CatalogSearchRequest()
        {

        }

        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }

            set
            {
                _searchTerm = value;
            }
        }
    }
}
