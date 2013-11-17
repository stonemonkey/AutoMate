using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ui.Wp8.Components.RecordingPage
{
    public class SpeedToAngleConverter: IValueConverter
    {
        private const double startAngle = 45;
        private const double stopAngle = 315;
        private const double topSpeed = 72.22;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var speed = (double)value;            
            if (double.IsNaN(speed)) return 0;
            
            var result = speed * stopAngle / topSpeed + startAngle;
            return (int) result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var angle = (double)value - startAngle;
            return angle * topSpeed / stopAngle;
        }
    }
}