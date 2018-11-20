using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using IUR_p07.Support;
using WeatherNet;
using WeatherNet.Clients;

namespace IUR_p07.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        private ICommand addCommand;
        private readonly List<string> cities = new List<string> {"Praha", "Brno", "Ostrava", "Jihlava", "Znojmo", "Vimperk"};


        private ICommand removeCommand;

        private WeatherCardViewModel selectedLocation;

        public ViewModel()
        {
            ClientSettings.ApiKey = "97707f8247d843415d2ebfe8a6be4987";

            foreach (var city in cities) _addCity(city);
        }

        public ObservableCollection<WeatherCardViewModel> WeatherCards { get; set; } = new ObservableCollection<WeatherCardViewModel>();

        public WeatherCardViewModel SelectedLocation
        {
            get => selectedLocation;
            set => SetProperty(ref selectedLocation, value);
        }

        public string NewLocation { get; set; }

        public ICommand AddCommand => addCommand ?? (addCommand = new RelayCommand(AddAction));
        public ICommand RemoveCommand => removeCommand ?? (removeCommand = new RelayCommand(RemoveAction));

        public void ChangeLocation()
        {
            Console.WriteLine("CHANGE");
            Console.WriteLine(SelectedLocation.Location);
        }

        public void RemoveAction(object o)
        {
            Console.WriteLine(SelectedLocation);
        }

        public void AddAction(object o)
        {
            _addCity(NewLocation);
        }

        private void _addCity(string city)
        {
            var result = CurrentWeather.GetByCityName(city, "Czechia", "en", "metric");

            if (!result.Success) return;

            var location = new WeatherCardViewModel
            {
                Location = result.Item.City, Temperature = result.Item.Temp, Humidity = result.Item.Humidity, Conditions = result.Item.Title,
                IconPath = result.Item.Icon
            };
            WeatherCards.Add(location);
        }
    }
}