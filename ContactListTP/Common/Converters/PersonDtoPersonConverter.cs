using System.Linq;
using Common.Dto;
using Google.Apis.PeopleService.v1.Data;

namespace Common.Converters
{
    public class PersonDtoPersonConverter : IConverter<IPersonDto, Person>
    {
        public IPersonDto Convert(Person person) => new PersonDto
        {
            FirstName = person.Names?.FirstOrDefault()?.GivenName ?? string.Empty,
            LastName = person.Names?.FirstOrDefault()?.FamilyName ?? string.Empty,
            PhoneNumber = person.PhoneNumbers?.FirstOrDefault()?.Value ?? person.PhoneNumbers?.FirstOrDefault()?.CanonicalForm ?? string.Empty,
            Address = person.Addresses?.FirstOrDefault()?.FormattedValue ?? string.Empty,
            BirthDayFormated = person.Birthdays?.FirstOrDefault()?.Date?.FormatToString() ?? person.Birthdays?.FirstOrDefault()?.Text ?? string.Empty,
            EmailAddress = person.EmailAddresses?.FirstOrDefault()?.Value ?? string.Empty,
            PhotoUrl = person.Photos?.FirstOrDefault()?.Url ?? string.Empty,
            ResourceName = person.ResourceName
        };

        public Person Convert(IPersonDto source) => PersonBuilder.Build(source);
    }
}