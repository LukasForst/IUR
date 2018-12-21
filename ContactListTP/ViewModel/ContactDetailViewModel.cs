using Common.Dto;
using Common.Extensions;
using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase
    {
        private const string DefaultDisplayString = "n/a";
        private readonly PersonDto personDto;

        private bool editModeEnabled;

        public ContactDetailViewModel(IPersonDto personDto)
        {
            this.personDto = new PersonDto(personDto);
        }

        public string DisplayedName
        {
            get
            {
                var str = "";
                if (!personDto.FirstName.IsNullOrEmpty()) str += $"{personDto.FirstName} ";

                if (!personDto.LastName.IsNullOrEmpty()) str += $"{personDto.LastName}";

                return str;
            }
        }

        public bool EditModeEnabled
        {
            get => editModeEnabled;
            set => SetProperty(ref editModeEnabled, value);
        }

        public string FirstName
        {
            get => personDto.FirstName;
            set => (personDto.FirstName = ValidateAndReturnValue(value, personDto.FirstName)).Also(() =>
            {
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(DisplayedName));
            });
        }

        public string LastName
        {
            get => personDto.LastName;
            set => (personDto.LastName = ValidateAndReturnValue(value, personDto.LastName)).Also(() =>
            {
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(DisplayedName));
            });
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

        public string BirthDayFormatted
        {
            get => personDto.BirthDayFormatted;
            set => (personDto.BirthDayFormatted = ValidateAndReturnValue(value, personDto.BirthDayFormatted))
                .Also(() => OnPropertyChanged(nameof(BirthDayFormatted)));
        }

        public string Address
        {
            get => personDto.Address;
            set => (personDto.Address = ValidateAndReturnValue(value, personDto.Address)).Also(() => OnPropertyChanged(nameof(Address)));
        }

        public IPersonDto GetPersonDto()
        {
            return new PersonDto(personDto);
        }

        private string ValidateAndReturnValue(string valueToSet, string previousValue)
        {
            if (valueToSet.IsNullOrEmpty() || valueToSet == DefaultDisplayString) return previousValue;
            return valueToSet;
        }
    }
}