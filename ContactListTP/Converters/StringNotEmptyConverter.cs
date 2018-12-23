using System;
using System.Globalization;
using System.Windows.Data;

namespace ContactListTP.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return ((string) value).Length != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}