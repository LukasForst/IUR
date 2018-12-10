using ContactListTP.ViewModel;
using IurGoogleApi.Contacts;
using Ninject.Modules;

namespace ContactListTP.Configuration
{
    public class DependencyInjectionConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IContactsProvider>().To<ContactsProvider>().InSingletonScope();

            Bind<MyViewModel>().ToMethod(x => new MyViewModel {NewLocation = "Hello World!"}).InTransientScope();
        }
    }
}