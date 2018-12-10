using System.Collections.Generic;
using System.Linq;
using Google.Apis.PeopleService.v1.Data;
using IurGoogleApi.Dto;

namespace IurGoogleApi.Contacts
{
    public class ContactsProvider : IContactsProvider
    {
        private readonly ApiService apiService;

        public ContactsProvider(ApiService apiService)
        {
            this.apiService = apiService;
        }

        public IReadOnlyCollection<IPersonDto> GetContacts() => apiService.ObtainPeopleFromApi().Select(Convert).ToList();

        public IPersonDto AddContact(IPersonDto personDto) => Convert(apiService.AddPerson(personDto));

        private IPersonDto Convert(Person person) => new PersonDto
        {
            FirstName = person.Names.Count > 0 ? person.Names.First().GivenName : "n/a",
            LastName = person.Names.Count > 0 ? person.Names.First().FamilyName : "n/a",
            PhoneNumbers = person.PhoneNumbers.Select(x => x.FormattedType).ToList(),
            EmailAddresses = person.EmailAddresses.Select(x => x.FormattedType).ToList()
        };
    }
}