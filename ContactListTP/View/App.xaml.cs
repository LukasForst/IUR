using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using ContactListTP.Configuration;
using IurGoogleApi.Extensions;
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

        public App()
        {
            DispatcherUnhandledException += UnhandledExceptionHandler;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Info("Starting app");
            base.OnStartup(e);

            iocKernel = new StandardKernel();
            iocKernel.Load(new DependencyInjectionConfig());

            Current.MainWindow = iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
            Log.Info("Application started!");
        }

        private void UnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is GoogleApiException googleException)
            {
                Log.Error("Google API connection failed!", googleException.OriginalException);
                var result = MessageBox.Show(
                    $"There are problems with Google API. \nWould you like to restart application?\n\n{googleException.OriginalException.Message}",
                    "Google API error", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result != MessageBoxResult.Yes) Current.Shutdown();
            }
            else
            {
                Log.Error("Exception occured in application!", e.Exception);
                MessageBox.Show("Application encountered exception!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.Warn("Application will shutdown.");
                Current.Shutdown();
            }
        }

        private void Restart()
        {
            Log.Warn("Application will restart!");
            Process.Start(ResourceAssembly.Location);
            Current.Shutdown();
        }
    }
}