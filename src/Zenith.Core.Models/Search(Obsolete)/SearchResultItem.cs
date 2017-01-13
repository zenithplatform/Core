using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.VirtualObservatory.Objects;

namespace Zenith.Core.Models
{
    public class SearchResultItem
    {
        public string Name { get; set; }
        public List<ValueBase> Values { get; set; }
    }
}
