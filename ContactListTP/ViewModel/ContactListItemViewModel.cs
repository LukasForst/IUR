using System;
using System.Windows.Documents;
using Common.Dto;

namespace ContactListTP.ViewModel
{
    public class ContactListItemViewModel : ListItem
    {
        private readonly IPersonDto personDto;
        private readonly Func<string, string, string> nameTransformation;

        public ContactListItemViewModel(IPersonDto personDto, Func<string, string, string> nameTransformation = null)
        {
            this.personDto = personDto;
            this.nameTransformation = nameTransformation ?? DefaultNameTransformation;
        }

        public string DisplayedName => nameTransformation(personDto.FirstName, personDto.LastName);

        public string PhotoUrl => personDto.PhotoUrl;

        public IPersonDto GetPersonDto() => new PersonDto(personDto);

        private string DefaultNameTransformation(string firstName, string familyName) => $"{firstName} {familyName}";
    }
}