using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ui.Wp8.Infrastructure
{
    public class GpsDataAquisition: IDataAquisition
    {
        private IAgresivityCalculator _calculator;
        private GeoCoordinateWatcher _watcher;
        
        public GpsDataAquisition(IAgresivityCalculator calculator, double movementThreshold){
            _calculator = calculator;
            _watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            _watcher.MovementThreshold = movementThreshold;
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