using System.Collections.Generic;
using System.Linq;
using ContactListTP.ViewModel;
using IurGoogleApi.Contacts;
using IurGoogleApi.Dto;

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
            contactsProvider.GetContacts().Select(x => new ContactListItemViewModel(x)).ToList();

        public ContactDetailViewModel AddContact(AddContactViewModel addContactViewModel)
        {
            var personDto = contactsProvider.AddContact(new PersonDto
            {
                FirstName = addContactViewModel.FirstName,
                LastName = addContactViewModel.LastName,
                EmailAddresses = new List<string> {addContactViewModel.EmailAddress},
                PhoneNumbers = new List<string> {addContactViewModel.PhoneNumber}
            });

            return new ContactDetailViewModel(personDto);
        }

        public IReadOnlyCollection<ContactListItemViewModel> RemoveContact(ContactDetailViewModel contactToRemove)
        {
            contactsProvider.RemoveContact(new PersonDto
            {
                FirstName = contactToRemove.FirstName,
                LastName = contactToRemove.LastName,
                EmailAddresses = new List<string> {contactToRemove.EmailAddress},
                PhoneNumbers = new List<string> {contactToRemove.PhoneNumber}
            });

            return BuildContactList();
        }
    }
}