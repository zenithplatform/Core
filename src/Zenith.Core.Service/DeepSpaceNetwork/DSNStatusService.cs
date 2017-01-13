using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.Models.DeepSpaceNetwork;
using Zenith.Core.Service.DeepSpaceNetwork;

namespace Zenith.Core.Service
{
    public class DSNStatusService
    {
        private Loader _loader = null;

        public DSNStatus GetStatus()
        {
            _loader = new Loader();
            _loader.Load();

            return _loader.Status; 
        }
    }
}
