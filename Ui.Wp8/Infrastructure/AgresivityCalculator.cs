using System;

namespace Ui.Wp8.Infrastructure
{
    public class AgresivityCalculator: IAgresivityCalculator
    {
        private readonly Random _random = new Random();

        public void AddAcceleration(Acceleration acceleration)
        {
        }

        public void AddGpsData(GpsData gpsData)
        {
        }

        public int Agresivity()
        {
            return _random.Next(1, 100);
        }
    }
}