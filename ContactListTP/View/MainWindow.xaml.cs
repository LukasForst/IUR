using ContactListTP.ViewModel;

namespace ContactListTP.View
{
    /// <inheritdoc cref="App" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        
        public MainWindow(MyViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}