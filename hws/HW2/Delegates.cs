
namespace HW2
{
    /// <summary>
    /// Delegate that should return weather data for given city
    /// </summary>
    /// <param name="cityName"></param>
    public delegate WeatherData WeatherProviderDelegate(string cityName);

    /// <summary>
    /// Delegate that returns localized weather data info
    /// </summary>
    /// <param name="data"></param>
    public delegate string TranslatingDelegate(WeatherData data);
}