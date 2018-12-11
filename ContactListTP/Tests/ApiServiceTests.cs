using System;
using System.Linq;
using Common.Dto;
using IurGoogleApi.Contacts;
using IurGoogleApi.Credentials;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ApiServiceTests
    {
        private const string PathToSecretes = "";

        private CredentialsProvider credentialsProvider;
        private ApiService apiService;

        [SetUp]
        public void SetUp()
        {
            credentialsProvider = new CredentialsProvider(PathToSecretes);
            apiService = new ApiService(credentialsProvider);
        }

        [Test]
        public void GetContacts()
        {
            var people = apiService.ObtainPeopleFromApi();
            foreach (var person in people)
            {
                Console.Write(person.Names != null ? person.Names[0].DisplayName + "  " : "n/a  ");
                Console.Write(person.EmailAddresses.FirstOrDefault()?.Value ?? "n/a" + "  ");
                Console.WriteLine(person.PhoneNumbers?.FirstOrDefault()?.Value ?? "n/a");
            }
        }

        [Test]
        public void AddContact()
        {
            var personDto = new PersonDto
            {
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "111 222 333",
                EmailAddress = "lomikar@boo.com"
            };
            var person = apiService.AddPerson(personDto);
            Console.Write(person.Names != null ? person.Names[0].DisplayName + "  " : "n/a  ");
            Console.Write(person.EmailAddresses.FirstOrDefault()?.Value ?? "n/a" + "  ");
            Console.WriteLine(person.PhoneNumbers?.FirstOrDefault()?.Value ?? "n/a");
        }
    }
}