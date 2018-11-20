using IUR_p07.Support;

namespace IUR_p07.ViewModels
{
    /// <summary>
    /// Class representing simplified (without notification) view model
    /// </summary>
    public class WeatherCardViewModel : ViewModelBase
    {
        public string Location { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public string Conditions { get; set; }
        public string IconPath { get; set; }
    }
}