using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8.Components.RecordingPage
{
    public class AccelerationViewModel : Screen, IDataAquisition
    {
        Accelerometer _accelerometer; 
        IAgresivityCalculator _calculator;

        public uint ReportInterval { get; set; }

        public AccelerationViewModel(AgresivityCalculator calculator)
        {
            ReportInterval = 5;
            _calculator = calculator;
            _accelerometer = Accelerometer.GetDefault();
            _accelerometer.ReportInterval = ReportInterval;
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