using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using courseApplication.provider;

namespace courseApplication.view
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainFrame : Window
    {
        private bool isModifyingContent;

        public MainFrame()
        {
            InitializeComponent();
            Loaded += MainFrame_OnLoad;
        }

        private void MainFrame_OnLoad(object sender, RoutedEventArgs e) => RefreshView();

        public void RefreshView()
        {
            isModifyingContent = true;

            CitySelection.Items.Clear();
            CitiesWeatherProvider.Instance.GetCities().ForEach(x => CitySelection.Items.Add(new ComboBoxItem {Content = x}));

            CitySelection.SelectedIndex = -1;

            CurrentTemperatureLabel.Content = "-";
            CurrentHumidityLabel.Content = "-";

            TomorrowTemperatureLabel.Content = "-";
            TomorrowHumidityLabel.Content = "-";

            NextDayHumidityLabel.Content = "-";
            NextDayTemperatureLabel.Content = "-";

            isModifyingContent = false;
        }

        private void ManageCitiesButton_OnClick(object sender, RoutedEventArgs e)
        {
            new CityManager().BindModel(this).Show();
        }

        private void CitySelection_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isModifyingContent) return;

            var city = (string) ((ComboBoxItem) e.AddedItems[0]).Content;
            var valuesToSet = GetSortedValues(city);

            CurrentTemperatureLabel.Content = StringFromDouble(valuesToSet[0].Item1, "°C");
            CurrentHumidityLabel.Content = StringFromDouble(valuesToSet[0].Item2, "%");

            TomorrowTemperatureLabel.Content = StringFromDouble(valuesToSet[1].Item1, "°C");
            TomorrowHumidityLabel.Content = StringFromDouble(valuesToSet[1].Item2, "%");

            NextDayTemperatureLabel.Content = StringFromDouble(valuesToSet[2].Item1, "°C");
            NextDayHumidityLabel.Content = StringFromDouble(valuesToSet[2].Item2, "%");
        }

        private IList<Tuple<double?, double?>> GetSortedValues(string city)
        {
            var weatherData = CitiesWeatherProvider.Instance.GetWeatherData(city)?.Weather ?? new Dictionary<DateTime, WeatherRecord>();

            var valuesToSet = new List<Tuple<double?, double?>>();
            for (var date = DateTime.Today; date < DateTime.Today.AddDays(3); date = date.AddDays(1))
            {
                var data = weatherData.GetOrDefault(date, null);
                valuesToSet.Add(new Tuple<double?, double?>(data?.Temperature, data?.Humidity));
            }

            return valuesToSet;
        }

        private static string StringFromDouble(double? value, string suffix)
        {
            return value.HasValue ? $"{value.Value:0.00}" + suffix : "No value available.";
        }
    }
}