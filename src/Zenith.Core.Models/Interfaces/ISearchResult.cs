using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.Interfaces
{
    public interface IObjectSearchResult<T> : ISearchResultBase where T : class
    {
        List<T> Objects { get; set; }
    }

    public interface ICatalogSearchResult<T> : ISearchResultBase where T : class
    {
        List<T> Catalogs { get; set; }
    }

    public interface ISearchResultBase
    {
        
    }
}
