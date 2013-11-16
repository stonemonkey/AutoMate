using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;

namespace Ui.Wp8.Infrastructure
{
    public class AccelerationDataAquisition: IDataAquisition
    {
        IAgresivityCalculator _calculator;
        Accelerometer _accelerometer;

        public AccelerationDataAquisition(IAgresivityCalculator calculator, uint reportInterval)
        {
            _calculator = calculator;
            _accelerometer = Accelerometer.GetDefault();
            _accelerometer.ReportInterval = reportInterval;
        }

        void _accelerometer_ReadingChanged(Accelerometer sender, AccelerometerReadingChangedEventArgs e)
        {
            var acceleration = new Acceleration(e.Reading);
            _calculator.AddAcceleration(acceleration);
        }

        public void Start()
        {
            _accelerometer.ReadingChanged += _accelerometer_ReadingChanged;
        }

        public void Stop()
        {
            _accelerometer.ReadingChanged -= _accelerometer_ReadingChanged;
        }
    }
}