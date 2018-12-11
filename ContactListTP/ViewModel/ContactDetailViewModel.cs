using Common.Dto;
using Common.Extensions;
using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase
    {
        private const string DefaultDisplayString = "n/a";
        private readonly PersonDto personDto;

        public ContactDetailViewModel(IPersonDto personDto)
        {
            this.personDto = new PersonDto(personDto);
        }

        public IPersonDto GetPersonDto() => new PersonDto(personDto);

        public string FirstName
        {
            get => personDto.FirstName;
            set => (personDto.FirstName = ValidateAndReturnValue(value, personDto.FirstName)).Also(() => OnPropertyChanged(nameof(FirstName)));
        }

        public string LastName
        {
            get => personDto.LastName;
            set => (personDto.LastName = ValidateAndReturnValue(value, personDto.LastName)).Also(() => OnPropertyChanged(nameof(LastName)));
        }

        public string PhoneNumber
        {
            get => personDto.PhoneNumber;
            set => (personDto.PhoneNumber = ValidateAndReturnValue(value, personDto.PhoneNumber)).Also(() => OnPropertyChanged(nameof(PhoneNumber)));
        }

        public string EmailAddress
        {
            get => personDto.EmailAddress;
            set => (personDto.EmailAddress = ValidateAndReturnValue(value, personDto.EmailAddress)).Also(() => OnPropertyChanged(nameof(EmailAddress)));
        }

        public string PhotoUrl => personDto.PhotoUrl;

        public string BirthDayFormated
        {
            get => personDto.BirthDayFormated;
            set => (personDto.BirthDayFormated = ValidateAndReturnValue(value, personDto.BirthDayFormated))
                .Also(() => OnPropertyChanged(nameof(BirthDayFormated)));
        }

        public string Address
        {
            get => personDto.Address;
            set => (personDto.Address = ValidateAndReturnValue(value, personDto.Address)).Also(() => OnPropertyChanged(nameof(Address)));
        }

        private string ValidateAndReturnValue(string valueToSet, string previousValue)
        {
            if (valueToSet.IsNullOrEmpty() || valueToSet == DefaultDisplayString) return previousValue;
            return valueToSet;
        }
    }
}