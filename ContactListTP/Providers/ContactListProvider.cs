using System.Collections.Generic;
using System.Linq;
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

        public IReadOnlyCollection<ContactListItemViewModel> BuildContactList() =>
            contactsProvider.GetContacts()
                .Select(x => new ContactListItemViewModel(x))
                .OrderBy(x => x.DisplayedName)
                .ToList();

        public ContactDetailViewModel AddContact(AddContactViewModel addContactViewModel) =>
            contactsProvider.AddContact(addContactViewModel.ToPersonDto())
                .Let(x => new ContactDetailViewModel(x));

        public IReadOnlyCollection<ContactListItemViewModel> RemoveContact(ContactDetailViewModel contactToRemove)
        {
            contactsProvider.RemoveContact(contactToRemove.GetPersonDto());
            return BuildContactList();
        }
    }
}