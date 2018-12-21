using Common.Dto;
using ContactListTP.ViewModel;

namespace ContactListTP.Extensions
{
    public static class Extensions
    {
        public static IPersonDto ToPersonDto(this ContactDetailViewModel model)
        {
            return new PersonDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                BirthDayFormatted = model.BirthDayFormatted,
                Address = model.Address
            };
        }
    }
}