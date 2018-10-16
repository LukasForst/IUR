using System;
using System.Collections.Generic;
using System.Linq;
using WeatherNet;
using WeatherNet.Clients;
using WeatherNet.Model;

namespace HW1.provider
{
    public class WeatherProvider
    {
        public WeatherProvider()
        {
            ClientSettings.ApiKey = "97707f8247d843415d2ebfe8a6be4987";
        }

        public WeatherData GetWeatherForCity(string city)
        {
            var result = FiveDaysForecast.GetByCityName(city, "Czechia", "en", "metric");
            return result.Success
                ? new WeatherData(city, result.Items.GroupBy(x => x.Date.Date).ToDictionary(x => x.Key, x => new WeatherRecord(x.First())))
                : null;
        }
    }

    public class WeatherRecord
    {
        public double Temperature { get; }
        public double Humidity { get; }

        public WeatherRecord(double temperature, double humidity)
        {
            Temperature = temperature;
            Humidity = humidity;
        }
        
        public WeatherRecord(WeatherResult result)
        {
            Temperature = result.Temp;
            Humidity = result.Humidity;
        }
    }

    public class WeatherData
    {
        public string City { get; }

        public IDictionary<DateTime, WeatherRecord> Weather { get; }

        public WeatherData(string city, IDictionary<DateTime, WeatherRecord> weather)
        {
            City = city;
            Weather = weather;
        }
    }
}