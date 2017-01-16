using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Interop
{
    public interface IBridgeCallback
    {
        void Activate();
        void Deactivate();
        bool IsActive { get; }
    }
}
