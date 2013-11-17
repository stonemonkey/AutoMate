using System;
using System.Windows.Data;

namespace Ui.Wp8.Infrastructure
{
    public class AccelerationToWidthConverter: IValueConverter
    {
        private const int MaximumWidth = 400;
        private const double MaximumAcceleration = 2;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var acceleration = (double)value;
            int width = (int)((acceleration * MaximumWidth) / MaximumAcceleration);
            return Math.Abs(width);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var width = (double)value;
            double acceleration = (width * MaximumAcceleration) / MaximumWidth;
            return acceleration;
        }
    }
}