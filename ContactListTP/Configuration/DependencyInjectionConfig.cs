using ContactListTP.ViewModel;
using IurGoogleApi.Contacts;
using IurGoogleApi.Credentials;
using Ninject.Modules;

namespace ContactListTP.Configuration
{
    public class DependencyInjectionConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IContactsProvider>().To<ContactsProvider>().InSingletonScope();
            Bind<CredentialsProvider>().ToMethod(x => new CredentialsProvider()).InSingletonScope();

            Bind<ContactListViewModel>().ToSelf().InTransientScope();
            Bind<AddContactViewModel>().ToSelf().InTransientScope();
        }
    }
}