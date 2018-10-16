using System;
using WeatherConnectorLib;

namespace lab2
{
    public class View
    {
        private Model model;

        public View(Model model)
        {
            this.model = model;
            this.model.modelUpdated += ModelUpdateHandler;
        }

        private void ModelUpdateHandler(object sender, WeatherData data) => ShowData(data);
        
        private void ShowData(WeatherData data)
        {
            var dataPresentation = $"Current weather for city {data.City}, {data.Country}\n"
                                   + $"\tTemp {data.Temp}OC, max {data.TempMax}, min {data.TempMin}\n"
                                   + $"\tHum {data.Humidity}%";
            Console.WriteLine(dataPresentation);
        }
    }
}