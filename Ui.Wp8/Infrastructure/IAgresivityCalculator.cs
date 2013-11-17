using System.Device.Location;
using Windows.Devices.Sensors;
namespace Ui.Wp8.Infrastructure
{
    public interface IAgresivityCalculator
    {
        void AddAcceleration(AccelerometerReading acceleration);
        void AddGpsData(GeoCoordinate geoCoordinate);
        int Agresivity();
    }
}