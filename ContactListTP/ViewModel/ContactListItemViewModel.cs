using System;
using ContactListTP.Configuration;
using IurGoogleApi.Dto;

namespace ContactListTP.ViewModel
{
    public class ContactListItemViewModel : ViewModelBase
    {
        private readonly IPersonDto personDto;
        private Func<string, string, string> nameTransformation;

        public ContactListItemViewModel(IPersonDto personDto, Func<string, string, string> nameTransformation = null)
        {
            this.personDto = personDto;
            this.nameTransformation = nameTransformation ?? DefaultNameTransformation;
        }

        public string Name => personDto.FirstName;
        
        public IPersonDto GetBackingValue() => new PersonDto(personDto);

        private string DefaultNameTransformation(string firstName, string familyName) => $"{firstName} {familyName}";
    }
}