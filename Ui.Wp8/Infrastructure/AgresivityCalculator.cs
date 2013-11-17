using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace Ui.Wp8.Infrastructure
{
    public class AgresivityCalculator: IAgresivityCalculator
    {
        public void AddAcceleration(AccelerometerReading acceleration)
        {
            
        }

        public void AddGpsData(GeoCoordinate geoCoordinate)
        {
            
        }

        public int Agresivity()
        {
            return 0;
        }
    }
}