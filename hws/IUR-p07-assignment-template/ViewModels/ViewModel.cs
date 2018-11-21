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

        public string NewLocation { get; set; }

        public ViewModel()
        {
            ClientSettings.ApiKey = "97707f8247d843415d2ebfe8a6be4987";
            cities.ForEach(AddCity);
        }


        public RelayCommand<WeatherCardViewModel> AddCommand => new RelayCommand<WeatherCardViewModel>(x => AddCity(NewLocation));
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
            WeatherCards.Add(location);
        }
    }
}