using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using IUR_p05.Support;

namespace IUR_p05.ViewModels
{
    internal class AlarmItemViewModel : ViewModelBase
    {
        private string _alarmName;

        public string AlarmName
        {
            get { return _alarmName; }
            set
            {
                _alarmName = value;
                OnPropertyChanged(nameof(AlarmName));
            }
        }

        // TODO: Implement
    }


    internal enum AlarmType
    {
        MIN = 0,
        MAX = 1
    }

    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            var parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }

        #endregion
    }
}