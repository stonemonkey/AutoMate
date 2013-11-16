using System.Device.Location;
namespace Ui.Wp8.Infrastructure
{
    public class GpsData
    {
        GeoCoordinate _coordinate;

        public GpsData(GeoCoordinate coordinate)
        {
            _coordinate = coordinate;
        }
    }
}