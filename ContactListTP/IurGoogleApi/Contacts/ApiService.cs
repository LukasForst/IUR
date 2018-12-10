using System.Collections.Generic;
using System.Linq;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using IurGoogleApi.Credentials;
using IurGoogleApi.Dto;

namespace IurGoogleApi.Contacts
{
    public class ApiService
    {
        private readonly CredentialsProvider credentialsProvider;

        public ApiService(CredentialsProvider credentialsProvider)
        {
            this.credentialsProvider = credentialsProvider;
        }

        public IEnumerable<Person> ObtainPeopleFromApi()
        {
            var peopleRequest = CreateService().People.Connections.List("people/me");

            peopleRequest.PersonFields = new List<string>
            {
                "phoneNumbers",
                "emailAddresses",
                "names"
            };

            var requestResponse = peopleRequest.Execute();
            if (requestResponse != null && requestResponse.Connections.Count > 0) return requestResponse.Connections;
            return new List<Person>();
        }

        public Person AddPerson(IPersonDto personDto)
        {
            var contactToCreate = new Person
            {
                Names = new List<Name> {new Name {GivenName = personDto.FirstName, FamilyName = personDto.LastName}},
                EmailAddresses = personDto.EmailAddresses.Select(x => new EmailAddress {DisplayName = x}).ToList(),
                PhoneNumbers = personDto.PhoneNumbers.Select(x => new PhoneNumber {CanonicalForm = x, FormattedType = x}).ToList()
            };

            return new PeopleResource.CreateContactRequest(CreateService(), contactToCreate).Execute();
        }

        private PeopleServiceService CreateService() => new PeopleServiceService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credentialsProvider.ObtainCredential(),
            ApplicationName = "IUR-TP"
        });
    }
}