using System.Collections.Generic;
using System.Linq;

namespace courseApplication.provider
{
    public class CitiesWeatherProvider
    {
        private static CitiesWeatherProvider instance;

        public static CitiesWeatherProvider Instance => instance ?? (instance = new CitiesWeatherProvider());

        private CitiesWeatherProvider()
        {
            citiesWithWeather = new Dictionary<string, WeatherData>();
            weatherProvider = new WeatherProvider();
            Populate();
        }

        private readonly Dictionary<string, WeatherData> citiesWithWeather;

        private readonly WeatherProvider weatherProvider;

        private void Populate() => new List<string> {"Praha", "Brno", "Ostrava", "Liberec"}.ForEach(x => AddCity(x));

        public bool AddCity(string city)
        {
            var weather = weatherProvider.GetWeatherForCity(city);
            if (weather == null)
            {
                return false;
            }

            citiesWithWeather.Add(city, weather);
            return true;
        }

        public void DeleteCity(string city) => citiesWithWeather.Remove(city);

        public WeatherData GetWeatherData(string city) => citiesWithWeather.TryGetValue(city, out var result) ? result : null;

        public List<string> GetCities() => citiesWithWeather.Keys.ToList();
    }
}