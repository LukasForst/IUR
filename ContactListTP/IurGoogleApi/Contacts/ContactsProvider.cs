using System.Collections.Generic;
using System.Linq;
using Common.Converters;
using Common.Dto;

namespace IurGoogleApi.Contacts
{
    public class ContactsProvider : IContactsProvider
    {
        private readonly ApiService apiService;

        public ContactsProvider(ApiService apiService)
        {
            this.apiService = apiService;
        }

        public IReadOnlyCollection<IPersonDto> GetContacts() => apiService.ObtainPeopleFromApi().Select(x => x.ToDto()).ToList();

        public IPersonDto AddContact(IPersonDto personDto) => apiService.AddPerson(personDto)?.ToDto();

        public void RemoveContact(IPersonDto personDto) => apiService.DeletePerson(personDto);
    }
}