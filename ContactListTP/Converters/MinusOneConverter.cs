using System;
using System.Globalization;
using System.Windows.Data;

namespace ContactListTP.Converters
{
    [ValueConversion(typeof(int), typeof(int))]
    public class MinusOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (targetType != typeof(int))
                throw new InvalidOperationException("The target must be a integer!");

            return (int) value - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}