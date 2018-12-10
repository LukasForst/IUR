using System.Windows;
using ContactListTP.Configuration;
using Ninject;

namespace ContactListTP.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {   
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            IocKernel.Initialize(new DependencyInjectionConfig());
            base.OnStartup(e);
        }
    }
}