using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Converters;
using Common.Dto;
using Common.Extensions;
using Google.Apis.PeopleService.v1;
using Google.Apis.PeopleService.v1.Data;
using Google.Apis.Services;
using IurGoogleApi.Credentials;
using log4net;

namespace IurGoogleApi.Contacts
{
    public class ApiService
    {
        private const int MaxPageSize = 2000;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly CredentialsProvider credentialsProvider;

        public ApiService(CredentialsProvider credentialsProvider)
        {
            this.credentialsProvider = credentialsProvider;
        }

        public IEnumerable<Person> ObtainPeopleFromApi()
        {
            Log.Info("Fetching data from Google.");
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

            var requestResponse = ExecuteSafe(peopleRequest.Execute);
            if (requestResponse != null && requestResponse.Connections.Count > 0)
                return requestResponse.Connections.Also(x => Log.Info($"Data fetched successfully! - {x.Count} contacts fetched"));

            Log.Warn("No data fetched.");
            return new List<Person>();
        }

        public bool DeletePerson(IPersonDto personDto)
        {
            Log.Info($"Deleting person - {personDto.ResourceName}");
            var result = ExecuteSafe(new PeopleResource.DeleteContactRequest(CreateService(), personDto.ResourceName).Execute);
            return result != null;
        }

        public Person AddPerson(IPersonDto personDto)
        {
            var contactToCreate = PersonBuilder.Build(personDto);
            return ExecuteSafe(new PeopleResource.CreateContactRequest(CreateService(), contactToCreate).Execute);
        }

        private PeopleServiceService CreateService()
        {
            return new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentialsProvider.ObtainCredential(),
                ApplicationName = "IUR-TP"
            });
        }

        private T ExecuteSafe<T>(Func<T> block, T defaultValue = null) where T : class
        {
            try
            {
                return block();
            }
            catch (Exception e)
            {
                Log.Error("Exception was thrown while fetching data from Google!", e);
                return defaultValue;
            }
        }
    }
}