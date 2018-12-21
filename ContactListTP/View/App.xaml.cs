using System.Reflection;
using System.Windows;
using ContactListTP.Configuration;
using log4net;
using Ninject;

namespace ContactListTP.View
{
    /// <inheritdoc />
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IKernel iocKernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Info("Starting app");
            base.OnStartup(e);

            iocKernel = new StandardKernel();
            iocKernel.Load(new DependencyInjectionConfig());

            Current.MainWindow = iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}