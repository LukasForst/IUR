using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Util.Store;

namespace IurGoogleApi.Credentials
{
    public class CredentialsProvider
    {
        public UserCredential ObtainCredential()
        {
            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] {PeopleServiceService.Scope.Contacts},
                    "user", CancellationToken.None, new FileDataStore("People.Auth.Store")).Result;
            }
        }
    }
}