using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IUR_p07.converters
{
    public class WeatherApiImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"http://openweathermap.org/img/w/{value}.png", UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}