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
        private readonly Random _random = new Random();

        public void AddAcceleration(AccelerometerReading acceleration)
        {
        }

        public void AddGpsData(GeoCoordinate geoCoordinate)
        {
        }

        public int Agresivity()
        {
            return _random.Next(1, 100);
        }
    }
}