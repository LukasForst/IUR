using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Dto;
using Common.Extensions;
using ContactListTP.Extensions;
using ContactListTP.ViewModel;
using IurGoogleApi.Contacts;
using log4net;

namespace ContactListTP.Providers
{
    public class ContactListProvider
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ContactsProvider contactsProvider;

        public ContactListProvider(ContactsProvider contactsProvider)
        {
            this.contactsProvider = contactsProvider;
        }

        public IReadOnlyCollection<ContactDetailViewModel> BuildContactList()
        {
            return contactsProvider.GetContacts()
                .Select(x => new ContactDetailViewModel(x))
                .OrderBy(x => x.DisplayedName)
                .ToList();
        }

        public ContactDetailViewModel SaveContact(ContactDetailViewModel addContactViewModel)
        {
            var dto = addContactViewModel.GetPersonDto();
            var saved = dto?.GoogleId == null
                ? contactsProvider.AddContact(addContactViewModel.ToPersonDto()).Let(x => new ContactDetailViewModel(x))
                : contactsProvider.UpdateContact(dto).Let(x => new ContactDetailViewModel(x));

            return saved;
        }

        public void RemoveContact(ContactDetailViewModel contactToRemove)
        {
            if (contactToRemove.GetPersonDto()?.GoogleId != null)
                contactsProvider.RemoveContact(contactToRemove.GetPersonDto());
            else
                Log.Warn("Contact could not be removed, because it is not saved!");
        }

        public ContactDetailViewModel CreateEmpty()
        {
            var dto = new PersonDto();
            return new ContactDetailViewModel(dto) {EditModeEnabled = true};
        }
    }
}