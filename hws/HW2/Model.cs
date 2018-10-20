using System;

namespace HW2
{
    public class Model
    {
        private WeatherData weatherData;
        private string city;

        /// <summary>
        /// Event that will be triggered when new weather data were received
        /// </summary>
        public EventHandler<WeatherData> ModelUpdated;

        /// <summary>
        /// How to get weather data?
        /// </summary>
        public WeatherProviderDelegate WeatherProviderDelegate { get; set; } = x => WeatherProvider.GetWeatherForCity(x, TemperatureUnit.C);

        /// <summary>
        /// Current city
        /// </summary>
        public string City
        {
            get => city;
            set
            {
                city = value;
                weatherData = WeatherProviderDelegate?.Invoke(city);
                ModelUpdated?.Invoke(this, weatherData);
                ModelUpdated += null;
            }
        }

        public Model(string city)
        {
            this.city = city;
        }

        public Model()
        {
        }
    }
}