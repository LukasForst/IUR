using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Util.Store;

namespace IurGoogleApi.Credentials
{
    public class CredentialsProvider
    {
        private const string DefaultSecretFilePath = "client_id.json";

        private readonly string pathToClientSecrets;

        public CredentialsProvider(string pathToClientSecrets = null)
        {
            this.pathToClientSecrets = pathToClientSecrets ?? DefaultSecretFilePath;
        }

        public UserCredential ObtainCredential()
        {
            using (var stream = new FileStream(pathToClientSecrets, FileMode.Open, FileAccess.Read))
            {
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] {PeopleServiceService.Scope.Contacts},
                    "user", CancellationToken.None, new FileDataStore("People.Auth.Store")).Result;
            }
        }
    }
}