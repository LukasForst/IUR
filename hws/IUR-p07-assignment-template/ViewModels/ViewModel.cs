using System.Collections.Generic;
using System.Collections.ObjectModel;
using IUR_p07.Support;
using WeatherNet;
using WeatherNet.Clients;

namespace IUR_p07.ViewModels
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<WeatherCardViewModel> WeatherCards { get; set; } = new ObservableCollection<WeatherCardViewModel>();

        private readonly List<string> cities = new List<string> {"Praha", "Brno", "Ostrava", "Jihlava", "Znojmo", "Vimperk"};

        private string newLocation;
        public string NewLocation
        {
            get => newLocation;
            set
            {
                if(newLocation == value) return;
                newLocation = value;
                OnPropertyChanged(nameof(NewLocation));
            }
            
        }

        public ViewModel()
        {
            ClientSettings.ApiKey = "97707f8247d843415d2ebfe8a6be4987";
            cities.ForEach(AddCity);
        }

        public RelayCommand<WeatherCardViewModel> AddCommand => new RelayCommand<WeatherCardViewModel>(x =>
        {
            AddCity(NewLocation);
            NewLocation = string.Empty;
        });
        
        public RelayCommand<WeatherCardViewModel> DeleteCommand => new RelayCommand<WeatherCardViewModel>(x => WeatherCards.Remove(x));

        private void AddCity(string city)
        {
            var result = CurrentWeather.GetByCityName(city, "Czechia", "en", "metric");
            if (!result.Success) return;

            var location = new WeatherCardViewModel
            {
                Location = result.Item.City,
                Temperature = result.Item.Temp,
                Humidity = result.Item.Humidity,
                Conditions = result.Item.Title,
                IconPath = result.Item.Icon
            };
            location.ModelChanged += UpdateForecast;
            WeatherCards.Add(location);
        }
        
        private void UpdateForecast(WeatherCardViewModel card)
        {
            var result = CurrentWeather.GetByCityName(card.Location, "Czechia", "en", "metric");
            if (!result.Success) return;
            
            card.Temperature = result.Item.Temp;
            card.Humidity = result.Item.Humidity;
            card.Conditions = result.Item.Title;
            card.IconPath = result.Item.Icon;
        }

    }
}