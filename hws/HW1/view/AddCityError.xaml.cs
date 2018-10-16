using System.Windows;

namespace HW1.view
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
