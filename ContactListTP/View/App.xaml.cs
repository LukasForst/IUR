using System.Windows;
using ContactListTP.Configuration;
using Ninject;

namespace ContactListTP.View
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IKernel iocKernel;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            iocKernel = new StandardKernel();
            iocKernel.Load(new DependencyInjectionConfig());

            Current.MainWindow = iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}