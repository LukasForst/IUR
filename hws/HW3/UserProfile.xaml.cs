using System.Windows;

namespace HW3
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    ///     Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private readonly Data data;
        
        public UserProfile()
        {
            InitializeComponent();
            data = new Data();
            DataContext = data;
        }


        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            data.AddInterest();
            EnteringInterestTextBox.Text = "";
        }


        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = InterestsListBox.SelectedItem;
            if (selectedItem != null)
            {
                data.InterestsList.Remove(selectedItem.ToString());
            }
        }
    }
}