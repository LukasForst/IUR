namespace Common.Dto
{
    public class PersonDto : IPersonDto
    {
        public PersonDto()
        {
        }

        public PersonDto(IPersonDto dto)
        {
            ResourceName = dto.ResourceName;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            PhoneNumber = dto.PhoneNumber;
            EmailAddress = dto.EmailAddress;
            PhotoUrl = dto.PhotoUrl;
            BirthDayFormated = dto.BirthDayFormated;
            Address = dto.Address;
        }

        public string ResourceName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string BirthDayFormated { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}