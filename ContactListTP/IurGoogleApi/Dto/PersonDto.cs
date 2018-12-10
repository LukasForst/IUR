using System;
using System.Collections.Generic;

namespace IurGoogleApi.Dto
{
    public class PersonDto : IPersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IReadOnlyCollection<string> PhoneNumbers { get; set; }

        public IReadOnlyCollection<string> EmailAddresses { get; set; }
    }
}