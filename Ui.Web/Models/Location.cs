using System.Collections.Generic;

namespace Ui.Web.Models
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class County
    {
        public string Name { get; set; }
        public List<Location> Locations { get; set; }
    }
}