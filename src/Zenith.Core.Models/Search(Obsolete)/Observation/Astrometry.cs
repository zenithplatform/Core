using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models
{
    public class Astrometry: ObservationDataCategory
    {
        public override string CategoryName
        {
            get
            {
                return "Astrometry";
            }
        }
    }
}
