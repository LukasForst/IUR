using System.Collections.Generic;
using Common.Converters;
using Common.Dto;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using IurGoogleApi.Credentials;

namespace IurGoogleApi.Contacts
{
    public class ApiService
    {
        private readonly CredentialsProvider credentialsProvider;
        private const int MaxPageSize = 2000;

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
                "names",
                "photos",
                "birthdays",
                "addresses"
            };
            peopleRequest.PageSize = MaxPageSize;

            var requestResponse = peopleRequest.Execute();
            if (requestResponse != null && requestResponse.Connections.Count > 0) return requestResponse.Connections;
            return new List<Person>();
        }

        public void DeletePerson(IPersonDto personDto)
        {
            new PeopleResource.DeleteContactRequest(CreateService(), personDto.ResourceName).Execute();
        }

        public Person AddPerson(IPersonDto personDto)
        {
            var contactToCreate = PersonBuilder.Build(personDto);
            return new PeopleResource.CreateContactRequest(CreateService(), contactToCreate).Execute();
        }

        private PeopleServiceService CreateService() => new PeopleServiceService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credentialsProvider.ObtainCredential(),
            ApplicationName = "IUR-TP"
        });
    }
}