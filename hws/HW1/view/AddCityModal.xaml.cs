using System.Windows;
using System.Windows.Input;
using HW1.provider;

namespace HW1.view
{
    public partial class AddCityModal : Window
    {
        private CityManager cityManager;

        public AddCityModal()
        {
            InitializeComponent();
            Loaded += AddCityModal_Loaded;
        }

        public AddCityModal BindModel(CityManager cityMngr)
        {
            cityManager = cityMngr;
            return this;
        }

        private void AddCityModal_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(CityNameTextBox);
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CitiesWeatherProvider.Instance.AddCity(CityNameTextBox.Text))
            {
                cityManager?.RefreshView();
                Close();
            }
            else new AddCityError().Show();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}