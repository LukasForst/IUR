using System.Windows.Input;
using ContactListTP.ViewModel;

namespace ContactListTP.View
{
    /// <inheritdoc cref="App" />
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(ContactListViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    ((ContactListViewModel) DataContext).NextCommand.Execute(null);
                    break;
                case Key.Up:
                    ((ContactListViewModel) DataContext).PreviousCommand.Execute(null);
                    break;
                case Key.Right:
                    ((ContactListViewModel) DataContext).NextCommand.Execute(null);
                    break;
                case Key.Left:
                    ((ContactListViewModel) DataContext).PreviousCommand.Execute(null);
                    break;
            }
        }
    }
}