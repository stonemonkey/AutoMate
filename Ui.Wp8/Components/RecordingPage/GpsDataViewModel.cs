using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Ui.Wp8.Infrastructure;
using System.Device.Location;

namespace Ui.Wp8.Components.RecordingPage
{
    public class GpsDataViewModel : Screen, IDataAquisition
    {
        private GeoCoordinateWatcher _watcher; 
        IAgresivityCalculator _calculator;

        public double MovementThreshold { get; set; }

        public GpsDataViewModel(AgresivityCalculator calculator)
        {
            MovementThreshold = 5;
            _calculator = calculator;
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            _watcher.MovementThreshold = MovementThreshold;
            _watcher.PositionChanged += _watcher_PositionChanged;
        }

        private void _watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var gpsData = new GpsData(e.Position.Location);
            _calculator.AddGpsData(gpsData);
        }

        public void Start()
        {
            _watcher.Start();
        }

        public void Stop()
        {
            _watcher.Stop();
        }
    }
}