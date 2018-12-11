using Common.Dto;
using ContactListTP.ViewModel;

namespace ContactListTP.Extensions
{
    public static class Extensions
    {
        public static IPersonDto ToPersonDto(this AddContactViewModel model) => new PersonDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            EmailAddress = model.EmailAddress,
            BirthDayFormated = model.BirthDayFormated,
            Address = model.Address,
        };
    }
}