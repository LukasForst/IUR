using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using IUR_p05.Support;

namespace IUR_p05.ViewModels
{
    public class AlarmItemViewModel : ViewModelBase
    {
        private string _alarmName;

        public string AlarmName
        {
            get => _alarmName;
            set
            {
                _alarmName = value;
                OnPropertyChanged(nameof(AlarmName));
            }
        }

        private AlarmType _type;
        public AlarmType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private string _sliderValue;
        public string SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                OnPropertyChanged(nameof(SliderValue));
            }
        }

        private string _selectedImage;

        public string SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        private string _alarmMessage;

        public string AlarmMessage
        {
            get => _alarmMessage;
            set
            {
                _alarmMessage = value;
                OnPropertyChanged(nameof(AlarmMessage));
            }
        }

        public ObservableCollection<string> AlarmIcons { get; set; } = new ObservableCollection<string>();

        public AlarmItemViewModel()
        {
            new[] {"hot", "cold"}
                .SelectMany(x => Enumerable.Range(1, 3).Select(y => $"pack://application:,,,/Images/{x}{y}.png"))
                .ToList()
                .ForEach(AlarmIcons.Add);
        }
    }


    public enum AlarmType
    {
        Min = 0,
        Max = 1
    }

    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is string parameterString) || !Enum.IsDefined(value.GetType(), value))
                return DependencyProperty.UnsetValue;

            return Enum.Parse(value.GetType(), parameterString).Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(parameter is string parameterString)
            ? DependencyProperty.UnsetValue
            : Enum.Parse(targetType, parameterString);
    }
}