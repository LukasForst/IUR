using System.Collections.Generic;

namespace IurGoogleApi.Dto
{
    public interface IPersonDto
    {
        string FirstName { get; }

        string LastName { get; }

        IReadOnlyCollection<string> PhoneNumbers { get; }

        IReadOnlyCollection<string> EmailAddresses { get; }
    }
}