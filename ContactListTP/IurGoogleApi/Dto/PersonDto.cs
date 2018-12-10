using System.Collections.Generic;

namespace IurGoogleApi.Dto
{
    public class PersonDto : IPersonDto
    {
        public PersonDto() {}

        public PersonDto(IPersonDto dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            PhoneNumbers = new List<string>(dto.PhoneNumbers);
            EmailAddresses = new List<string>(dto.EmailAddresses);
        }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IReadOnlyCollection<string> PhoneNumbers { get; set; }

        public IReadOnlyCollection<string> EmailAddresses { get; set; }
    }
}