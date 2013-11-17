using System;
using System.Windows.Data;

namespace Ui.Wp8.Infrastructure
{
    public class SpeedToAngleConverter: IValueConverter
    {
        private const double StartAngle = 45;
        private const double StopAngle = 315;
        private const double TopSpeed = 72.22;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var speed = (double)value;            
            if (double.IsNaN(speed)) return 0;
            
            var result = speed * StopAngle / TopSpeed + StartAngle;
            return (int) result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var angle = (double)value - StartAngle;
            return angle * TopSpeed / StopAngle;
        }
    }
}