using System.Windows;

namespace courseApplication.view
{
    public partial class AddCityError : Window
    {
        public AddCityError()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
