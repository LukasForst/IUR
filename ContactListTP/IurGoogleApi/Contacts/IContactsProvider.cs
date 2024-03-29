using System.Collections.Generic;
using Common.Dto;

namespace IurGoogleApi.Contacts
{
    public interface IContactsProvider
    {
        IReadOnlyCollection<IPersonDto> GetContacts();

        IPersonDto AddContact(IPersonDto personDto);

        bool RemoveContact(IPersonDto personDto);
    }
}