namespace Common.Dto
{
    public class PersonDto : IPersonDto
    {
        public PersonDto()
        {
        }

        public PersonDto(IPersonDto dto)
        {
            if (dto.GoogleId != null) GoogleId = new GoogleId(dto.GoogleId);
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            PhoneNumber = dto.PhoneNumber;
            EmailAddress = dto.EmailAddress;
            PhotoUrl = dto.PhotoUrl;
            BirthDayFormatted = dto.BirthDayFormatted;
            Address = dto.Address;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string BirthDayFormatted { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public IGoogleId GoogleId { get; set; }
    }
}