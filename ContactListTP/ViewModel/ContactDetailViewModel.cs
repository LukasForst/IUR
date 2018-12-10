using System.Linq;
using Common.Extensions;
using ContactListTP.Configuration;
using IurGoogleApi.Dto;

namespace ContactListTP.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase
    {
        private readonly PersonDto personDto;

        public ContactDetailViewModel(IPersonDto personDto)
        {
            this.personDto = new PersonDto(personDto);
        }

        public string FirstName
        {
            get => personDto.FirstName;
            set => (personDto.FirstName = value).Also(() => OnPropertyChanged(nameof(FirstName)));
        }
        
        public string LastName
        {
            get => personDto.LastName;
            set => (personDto.LastName = value).Also(() => OnPropertyChanged(nameof(LastName)));
        }
        
        public string PhoneNumber
        {
            get => personDto.PhoneNumbers.FirstOrDefault() ?? "n/a";
            set => (personDto.PhoneNumbers = new[] {value}).Also(() => OnPropertyChanged(nameof(PhoneNumber)));
        }
        
        public string EmailAddress
        {
            get => personDto.EmailAddresses.FirstOrDefault() ?? "n/a";
            set => (personDto.EmailAddresses = new[] {value}).Also(() => OnPropertyChanged(nameof(EmailAddress)));
        }
    }
}