using Common.Extensions;
using ContactListTP.Configuration;

namespace ContactListTP.ViewModel
{
    public class AddContactViewModel : ViewModelBase
    {
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string phoneNumber = string.Empty;
        private string emailAddress = string.Empty;
        private string birthDay = string.Empty;
        private string address = string.Empty;

        public string FirstName
        {
            get => firstName;
            set => (firstName = value).Also(() => OnPropertyChanged(nameof(FirstName)));
        }

        public string LastName
        {
            get => lastName;
            set => (lastName = value).Also(() => OnPropertyChanged(nameof(LastName)));
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => (phoneNumber = value).Also(() => OnPropertyChanged(nameof(PhoneNumber)));
        }

        public string EmailAddress
        {
            get => emailAddress;
            set => (emailAddress = value).Also(() => OnPropertyChanged(nameof(EmailAddress)));
        }
        
        public string BirthDayFormated
        {
            get => birthDay;
            set => (birthDay = value).Also(() => OnPropertyChanged(nameof(BirthDayFormated)));
        }

        public string Address
        {
            get => address;
            set => (address = value).Also(() => OnPropertyChanged(nameof(Address)));
        }

    }
}