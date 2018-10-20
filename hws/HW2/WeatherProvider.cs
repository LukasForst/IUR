using System;
using WeatherNet;
using WeatherNet.Clients;
using WeatherNet.Model;

namespace HW2
{
    public static class WeatherProvider
    {
        static WeatherProvider()
        {
            ClientSettings.ApiKey = "97707f8247d843415d2ebfe8a6be4987";
        }


        /// <summary>
        /// Returns weather for given city and given <see cref="TemperatureUnit"/>
        /// </summary>
        public static WeatherData GetWeatherForCity(string cityName, TemperatureUnit temperatureUnit)
        {
            switch (temperatureUnit)
            {
                case TemperatureUnit.F:
                    return new WeatherData(CurrentWeather.GetByCityName(cityName, "Czechia", "en", "imperial").Item, temperatureUnit);
                case TemperatureUnit.C:
                    return new WeatherData(CurrentWeather.GetByCityName(cityName, "Czechia", "en", "metric").Item, temperatureUnit);
                default:
                    throw new ArgumentOutOfRangeException(nameof(temperatureUnit), temperatureUnit, null);
            }
        }
    }

    public class WeatherData
    {
        public WeatherData(WeatherResult currentWeatherResult, TemperatureUnit tempUnit)
        {
            City = currentWeatherResult.City;
            Country = currentWeatherResult.Country;
            Temp = currentWeatherResult.Temp;
            TempMax = currentWeatherResult.TempMax;
            TempMin = currentWeatherResult.TempMin;
            Humidity = currentWeatherResult.Humidity;
            TempUnit = tempUnit;
        }

        public string City { get; }

        public double Temp { get; }

        public double TempMax { get; }

        public double TempMin { get; }

        public double Humidity { get; }

        public string Country { get; }

        public TemperatureUnit TempUnit { get; }
    }

    public enum TemperatureUnit
    {
        F,
        C
    }
}