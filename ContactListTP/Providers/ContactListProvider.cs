using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Common.Extensions;
using ContactListTP.Extensions;
using ContactListTP.ViewModel;
using IurGoogleApi.Contacts;

namespace ContactListTP.Providers
{
    public class ContactListProvider
    {
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
            return dto?.GoogleId == null
                ? contactsProvider.AddContact(addContactViewModel.ToPersonDto()).Let(x => new ContactDetailViewModel(x))
                : contactsProvider.UpdateContact(dto).Let(x => new ContactDetailViewModel(x));
        }

        public IReadOnlyCollection<ContactDetailViewModel> RemoveContact(ContactDetailViewModel contactToRemove)
        {
            contactsProvider.RemoveContact(contactToRemove.GetPersonDto());
            return BuildContactList();
        }

        public ContactDetailViewModel CreateEmpty()
        {
            var dto = new PersonDto();
            return new ContactDetailViewModel(dto) {EditModeEnabled = true};
        }
    }
}