using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models.VirtualObservatory
{
    public class VirtualObservatorySearchRequest : VirtualObservatoryRequestBase
    {
        public string SearchTerm { get; set; }
    }

    public class VirtualObservatoryEmptyRequest : VirtualObservatoryRequestBase
    {

    }

    public abstract class VirtualObservatoryRequestBase
    {

    }
}
