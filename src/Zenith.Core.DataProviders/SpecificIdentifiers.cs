using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith.Core.DataProviders.Base;

namespace Zenith.Core.DataProviders
{
    public class SimpleIdentifier : IdentifierBase
    {
        string _name = "";
        object _value = null;

        public SimpleIdentifier(string name, object value)
        {
            this._name = name;
            this._value = value;
        }

        public override string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public override object Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }

    public class SimpleIdentifierCollection: IndentifierCollection<SimpleIdentifier>
    {

    }
}
