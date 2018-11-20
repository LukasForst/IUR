using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IUR_p07.converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterBool = parameter != null && bool.Parse(parameter.ToString());
            return value != null && (bool) value != parameterBool ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}