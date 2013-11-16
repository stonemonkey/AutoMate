using Windows.Devices.Sensors;
namespace Ui.Wp8.Infrastructure
{
    public class Acceleration
    {
        private AccelerometerReading _reading;

        public Acceleration(AccelerometerReading reading){
            _reading = reading;
        }
    }
}