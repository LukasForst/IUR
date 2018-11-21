using System;
using IUR_p07.Support;

namespace IUR_p07.ViewModels
{
    /// <summary>
    /// Class representing simplified (without notification) view model
    /// </summary>
    public class WeatherCardViewModel : ViewModelBase
    {
        private string location;

        public string Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
                OnModelChanged(this);
            }
        }

        private double temperature;

        public double Temperature
        {
            get => temperature;
            set
            {
                if (Math.Abs(temperature - value) < 0.001) return;
                temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        private double humidity;

        public double Humidity
        {
            get => humidity;
            set
            {
                if(Math.Abs(humidity - value) < 0.001) return;
                humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }

        private string conditions;

        public string Conditions
        {
            get => conditions;
            set
            {
                if(conditions == value) return;
                conditions = value;
                OnPropertyChanged(nameof(Conditions));
            }
        }

        private string iconPath;

        public string IconPath
        {
            get => iconPath;
            set
            {
                if(iconPath == value) return;
                iconPath = value;
                OnPropertyChanged(nameof(IconPath));
            }
        }

        public event Action<WeatherCardViewModel> ModelChanged;

        private void OnModelChanged(WeatherCardViewModel obj)
        {
            ModelChanged?.Invoke(obj);
        }
    }
}