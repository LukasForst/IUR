using Common.Dto;
using Common.Extensions;
using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase
    {
        private const string DefaultDisplayString = "n/a";
        private readonly PersonDto personDto;

        private string _address;

        private string _birthDayFormatted;

        private string _emailAddress;

        private string _firstName;

        private string _lastName;

        private string _phoneNumber;

        private bool editModeEnabled;

        public ContactDetailViewModel(IPersonDto personDto)
        {
            this.personDto = new PersonDto(personDto);
            UpdateModelFromDto();
        }

        public string DisplayedName
        {
            get
            {
                var str = "";
                if (!FirstName.IsNullOrEmpty()) str += $"{FirstName} ";

                if (!LastName.IsNullOrEmpty()) str += $"{LastName}";

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
            get => _firstName;
            set => (_firstName = ValidateAndReturnValue(value, _firstName)).Also(() =>
            {
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(DisplayedName));
            });
        }

        public string LastName
        {
            get => _lastName;
            set => (_lastName = ValidateAndReturnValue(value, _lastName)).Also(() =>
            {
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(DisplayedName));
            });
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set => SetProperty(ref _emailAddress, value);
        }

        public string PhotoUrl => personDto.PhotoUrl.IsNullOrEmpty() ? null : personDto.PhotoUrl;

        public string BirthDayFormatted
        {
            get => _birthDayFormatted;
            set => SetProperty(ref _birthDayFormatted, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public Command<ContactListViewModel> RevertChanges => new Command<ContactListViewModel>(_ => UpdateModelFromDto(), _ => personDto != null);

        public IPersonDto GetPersonDto()
        {
            return new PersonDto(personDto);
        }

        public void ApplyChangeSet()
        {
            personDto.FirstName = FirstName;
            personDto.LastName = LastName;
            personDto.PhoneNumber = PhoneNumber;
            personDto.EmailAddress = EmailAddress;
            personDto.BirthDayFormatted = BirthDayFormatted;
            personDto.Address = Address;
        }

        private void UpdateModelFromDto()
        {
            FirstName = personDto.FirstName;
            LastName = personDto.LastName;
            PhoneNumber = personDto.PhoneNumber;
            EmailAddress = personDto.EmailAddress;
            BirthDayFormatted = personDto.BirthDayFormatted;
            Address = personDto.Address;
        }

        private string ValidateAndReturnValue(string valueToSet, string previousValue)
        {
            if (valueToSet.IsNullOrEmpty() || valueToSet == DefaultDisplayString) return previousValue;
            return valueToSet;
        }
    }
}