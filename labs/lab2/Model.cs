using System;
using WeatherConnectorLib;

namespace lab2
{
    public class Model
    {
        public WeatherData WeatherData { get; private set; }

        public EventHandler<WeatherData> modelUpdated;

        private string city;

        public string City
        {
            get => city;
            set
            {
                city = value;
                WeatherData = WeatherConnector.GetWeatherForCity(city);
                modelUpdated?.Invoke(this, WeatherData);
            }
        }


        public Model()
        {
            WeatherConnector.ApiKey = "97707f8247d843415d2ebfe8a6be4987";
        }
    }
}