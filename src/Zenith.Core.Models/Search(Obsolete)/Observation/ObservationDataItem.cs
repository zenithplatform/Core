using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Zenith.Core.Models
{
    public class ObservationDataItem
    {
        public ObservationDataItem()
        {
            
        }

        public string Name { get; set; }
        public string Designation { get; set; }
        public IEnumerable<ObservationValue> Values { get; set; }
    }
}
