using IurGoogleApi.Contacts;

namespace ContactListTP.View
{
    /// <inheritdoc cref="App" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(IContactsProvider contactsProvider)
        {
            InitializeComponent();
        }
    }
}