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
using IurGoogleApi.Extensions;
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

            var requestResponse = ExecuteChecking(peopleRequest.Execute);
            if (requestResponse != null && requestResponse.Connections.Count > 0)
                return requestResponse.Connections.Also(x => Log.Info($"Data fetched successfully! - {x.Count} contacts fetched"));

            Log.Warn("No data fetched.");
            return new List<Person>();
        }

        public bool DeletePerson(IPersonDto personDto)
        {
            Log.Info($"Deleting person - {personDto.GoogleId.ResourceName}");
            var result = ExecuteChecking(new PeopleResource.DeleteContactRequest(CreateService(), personDto.GoogleId.ResourceName).Execute);
            return result != null;
        }

        public Person AddPerson(IPersonDto personDto)
        {
            var contactToCreate = PersonBuilder.Build(personDto);
            return ExecuteChecking(new PeopleResource.CreateContactRequest(CreateService(), contactToCreate).Execute);
        }

        public Person UpdatePerson(IPersonDto personDto)
        {
            if (personDto?.GoogleId == null)
            {
                Log.Error("Cannot update person when no resource name provided!");
                return null;
            }

            var contactToUpdate = PersonBuilder.Build(personDto);
            contactToUpdate.ResourceName = personDto.GoogleId.ResourceName;
            contactToUpdate.ETag = personDto.GoogleId.ETag;
            var request = new PeopleResource.UpdateContactRequest(CreateService(), contactToUpdate, contactToUpdate.ResourceName)
            {
                UpdatePersonFields = new List<string>
                {
                    "phoneNumbers",
                    "emailAddresses",
                    "names",
                    "photos",
                    "birthdays",
                    "addresses"
                }
            };
            return ExecuteChecking(request.Execute);
        }

        private PeopleServiceService CreateService()
        {
            return new PeopleServiceService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentialsProvider.ObtainCredential(),
                ApplicationName = "IUR-TP"
            });
        }

        private T ExecuteChecking<T>(Func<T> block)
        {
            try
            {
                return block();
            }
            catch (Exception e)
            {
                Log.Error("Exception was thrown while fetching data from Google!", e);
                throw new GoogleApiException(e);
            }
        }
    }
}