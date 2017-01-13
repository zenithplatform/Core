using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models
{
    public class ObservationDataCategory
    {
        public IEnumerable<ObservationDataItem> CategoryItems { get; set; }
        public virtual string CategoryName { get; }
    }
}
