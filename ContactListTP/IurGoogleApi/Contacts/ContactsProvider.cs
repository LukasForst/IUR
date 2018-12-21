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

        public IReadOnlyCollection<IPersonDto> GetContacts()
        {
            return apiService.ObtainPeopleFromApi().Select(x => x.ToDto()).ToList();
        }

        public IPersonDto AddContact(IPersonDto personDto)
        {
            return apiService.AddPerson(personDto)?.ToDto();
        }

        public bool RemoveContact(IPersonDto personDto)
        {
            return apiService.DeletePerson(personDto);
        }

        public IPersonDto UpdateContact(IPersonDto personDto)
        {
            return apiService.UpdatePerson(personDto)?.ToDto();
        }
    }
}