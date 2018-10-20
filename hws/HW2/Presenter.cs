using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HW2.interfaces;

namespace HW2
{
    public class Presenter : IPresenter
    {
        private readonly Model model;
        private readonly List<IView> views = new List<IView>();

        private TranslatingDelegate formatter;

        public Presenter(Model model)
        {
            this.model = model;
            this.model.ModelUpdated += ModelUpdateHandler;

            formatter = EnglishFormat;
        }

        /// <summary>
        /// Registers View, where operation result should be displayed
        /// </summary>
        public void RegisterView(IView view) => views.Add(view);

        /// <summary>
        /// Deregisters view from Presenter
        /// </summary>
        public void DeregisterView(IView view) => views.Remove(view);

        private void ModelUpdateHandler(object sender, WeatherData data) => views.ForEach(x => x.ShowResponse(formatter(data)));

        private string EnglishFormat(WeatherData data) => $"Current weather for city {data.City}, {data.Country}\n"
                                                          + $"\tTemp {data.Temp} {data.TempUnit}°, max {data.TempMax} {data.TempUnit}°, min {data.TempMin} {data.TempUnit}°\n"
                                                          + $"\tHum {data.Humidity}%\n"
                                                          + "command: ";

        private string CzechFormat(WeatherData data) => $"Počasí v současné chvíli ve městě {data.City}, {data.Country}\n"
                                                        + $"\tTemplota {data.Temp} {data.TempUnit}°, maximum {data.TempMax} {data.TempUnit}°, minimum {data.TempMin} {data.TempUnit}°\n"
                                                        + $"\tVlhkost {data.Humidity}%\n"
                                                        + "příkaz: ";

        /// <summary>
        /// Handles command represented as string
        /// </summary>
        public void HandleCommand(string command)
        {
            command = command.Trim().ToLower();
            if (command.StartsWith("setunits"))
            {
                var unit = Regex.Match(command, "\\((.*?)\\)").Groups[1].Value;
                if (new ArrayList {"celsius", "c", "cels"}.Contains(unit))
                {
                    model.WeatherProviderDelegate = x => WeatherProvider.GetWeatherForCity(x, TemperatureUnit.C);
                }
                else if (new ArrayList {"fahrenheit", "f"}.Contains(unit))
                {
                    model.WeatherProviderDelegate = x => WeatherProvider.GetWeatherForCity(x, TemperatureUnit.F);
                }
            }
            else if (command.StartsWith("setlanguage"))
            {
                var value = Regex.Match(command, "\\((.*?)\\)").Groups[1].Value;
                if (new ArrayList {"cs", "czech", "cze"}.Contains(value))
                {
                    formatter = CzechFormat;
                }
                else if (new ArrayList {"en", "english", "eng"}.Contains(value))
                {
                    formatter = EnglishFormat;
                }
            }
            else if (command == "x")
            {
                Environment.Exit(0);
            }
            else
            {
                model.City = command;
            }
        }
    }
}