using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Ui.Wp8.Infrastructure;

namespace Ui.Wp8.Components.RecordingPage
{
    public class AccelerationToWidthConverter: IValueConverter
    {
        private const int maximumWidth = 400;
        private const double maximumAcceleration = 2;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var acceleration = (double)value;
            int width = (int)((acceleration * maximumWidth) / maximumAcceleration);
            return Math.Abs(width);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;
            double acceleration = (width * maximumAcceleration) / maximumWidth;
            return acceleration;
        }
    }
}