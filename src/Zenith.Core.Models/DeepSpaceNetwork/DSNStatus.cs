using System;
using System.Collections.Generic;

namespace Zenith.Core.Models.DeepSpaceNetwork
{
    public class DSNStatus
    {
        List<Site> _sites = new List<Site>();
        List<Spacecraft> _spacecrafts = new List<Spacecraft>();
        List<Dish> _dishes = new List<Dish>();

        public DSNStatus()
        {

        }

        public List<Spacecraft> Spacecrafts
        {
            get { return _spacecrafts; }
        }

        public List<Site> Sites
        {
            get { return _sites; }
        }

        public List<Dish> Dishes
        {
            get { return _dishes; }
        }

        public DateTime LastUpdated;
    }
}
