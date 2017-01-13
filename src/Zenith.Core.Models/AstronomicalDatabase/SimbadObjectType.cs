using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Models
{
    public class CelestialObjectType
    {
        ObservableCollection<CelestialObjectType> _subtypes = null;

        public CelestialObjectType()
        {
            _subtypes = new ObservableCollection<CelestialObjectType>();
        }

        public string Identifier { get; set; }
        public string ShortName { get; set; }
        public string ShortCode { get; set; }
        public string Description { get; set; }
        public ObservableCollection<CelestialObjectType> Subtypes { get { return _subtypes; } set { _subtypes = value; } }
    }

    public class ObjectTypesContainer
    {
        public ObjectTypesContainer()
        {
        }

        public ObservableCollection<CelestialObjectType> AllTypes { get; set; }
    }
}
