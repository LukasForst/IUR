using IurGoogleApi.Contacts;
using Ninject.Modules;

namespace ContactListTP.Configuration
{
    public class DependencyInjectionConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IContactsProvider>().To<ContactsProvider>().InSingletonScope(); // Reuse same storage every time
        }
    }
}