using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IUR_p07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<WeatherCardViewModel> WeatherCards { get; set; } = new ObservableCollection<WeatherCardViewModel>();
        public List<string> cities = new List<string>() { "Praha","Brno", "Ostrava", "Jihlava", "Znojmo", "Vimperk" };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            WeatherNet.ClientSettings.ApiUrl = "http://api.openweathermap.org/data/2.5/";
            WeatherNet.ClientSettings.ApiKey = "YOUR_API_KEY";

            foreach(string city in cities)
            {
                var result = WeatherNet.Clients.CurrentWeather.GetByCityName(city, "Czechia", "en", "metric");

                WeatherCards.Add(new WeatherCardViewModel() { Location = result.Item.City, Temperature = result.Item.Temp, Humidity = result.Item.Humidity, Conditions = result.Item.Title, IconPath = result.Item.Icon });
            }

        }
    }
}
