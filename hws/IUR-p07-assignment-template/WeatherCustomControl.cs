using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IUR_p07
{
    public class WeatherCustomControl : ToggleButton
    {
        static WeatherCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WeatherCustomControl), new FrameworkPropertyMetadata(typeof(WeatherCustomControl)));
        }

        /*
         * FINALIZE CUSTOM CONTROL
         */
    }

    public class BoolToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool parameterBool = bool.Parse(parameter.ToString());

            if ((bool)value != parameterBool)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class WeatherAPIImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = new Image();
            var fullFilePath = String.Format("http://openweathermap.org/img/w/{0}.png", value);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
            bitmap.EndInit();


            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}