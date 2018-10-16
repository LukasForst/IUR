using System.Windows;
using System.Windows.Controls;
using courseApplication.provider;

namespace courseApplication.view
{
    public partial class CityManager : Window
    {
        private readonly CitiesWeatherProvider citiesProvider;
        private MainFrame mainFrame;

        public CityManager()
        {
            InitializeComponent();
            citiesProvider = CitiesWeatherProvider.Instance;
            RefreshView();
        }

        public CityManager BindModel(MainFrame mainFrm)
        {
            mainFrame = mainFrm;
            return this;
        }

        private void AddCityButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AddCityModal().BindModel(this).Show();
        }

        private void DeleteCityButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = (string) ((ListViewItem) CitiesListView.SelectedItem).Content;
            citiesProvider.DeleteCity(selected);
            RefreshView();
        }

        public void RefreshView()
        {
            CitiesListView.Items.Clear();
            citiesProvider.GetCities().ForEach(x => CitiesListView.Items.Add(new ListViewItem {Content = x}));
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame?.RefreshView();
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            mainFrame?.RefreshView();
            Close();
        }
    }
}