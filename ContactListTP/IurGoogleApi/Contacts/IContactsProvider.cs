using System.Collections.Generic;
using IurGoogleApi.Dto;

namespace IurGoogleApi.Contacts
{
    public interface IContactsProvider
    {
        IReadOnlyCollection<IPersonDto> GetContacts();
        
        IPersonDto AddContact(IPersonDto personDto);
    }
}