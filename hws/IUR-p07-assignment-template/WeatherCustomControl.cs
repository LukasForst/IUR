using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace IUR_p07
{
    public class WeatherCustomControl : ToggleButton
    {
        static WeatherCustomControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WeatherCustomControl), new FrameworkPropertyMetadata(typeof(WeatherCustomControl)));
        }

        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register(nameof(RemoveCommand), typeof(ICommand),
            typeof(WeatherCustomControl), new PropertyMetadata(default(ICommand)));

        public ICommand RemoveCommand
        {
            get => (ICommand) GetValue(RemoveCommandProperty);
            set => SetValue(RemoveCommandProperty, value);
        }

        public static readonly DependencyProperty CityProperty = DependencyProperty.Register(nameof(City), typeof(string), typeof(WeatherCustomControl));

        public string City
        {
            get => (string) GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }

        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register(nameof(Temperature), typeof(int), typeof(WeatherCustomControl));

        public int Temperature
        {
            get => (int) GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }

        public static readonly DependencyProperty HumidityProperty = DependencyProperty.Register(nameof(Humidity), typeof(int), typeof(WeatherCustomControl));

        public int Humidity
        {
            get => (int) GetValue(HumidityProperty);
            set => SetValue(HumidityProperty, value);
        }

        public static readonly DependencyProperty ConditionsProperty =
            DependencyProperty.Register(nameof(Conditions), typeof(string), typeof(WeatherCustomControl));

        public string Conditions
        {
            get => (string) GetValue(ConditionsProperty);
            set => SetValue(ConditionsProperty, value);
        }

        public static readonly DependencyProperty
            IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(WeatherCustomControl));

        public string IconPath
        {
            get => (string) GetValue(IconPathProperty);
            set => SetValue(IconPathProperty, value);
        }
    }
}