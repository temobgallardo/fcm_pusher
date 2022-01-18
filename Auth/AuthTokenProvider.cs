using FirebasePusher.Auth.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;

/// <summary>
/// For this to work follow https://firebase.google.com/docs/admin/setup#c
/// </summary>
namespace FirebasePusher.Auth
{
  public class AuthTokenProvider : IAuthTokenProvider
  {
    private string credentials = "litio7-pushy-firebase-adminsdk-m0uji-1c39f542bc.json";
    private const string MESSAGING_SCOPE = "https://www.googleapis.com/auth/firebase.messaging";
    private string[] SCOPES = new string[] { MESSAGING_SCOPE };

    private readonly ServiceAccountCredential _credential;

    public AuthTokenProvider()
    {
      _credential = BuildCredentials();
    }

    private ServiceAccountCredential BuildCredentials()
    {
      if (GoogleCredential.FromFile(credentials)
                .CreateScoped(SCOPES)
                .UnderlyingCredential is not ServiceAccountCredential serviceAccountCredential)
      {
        throw new InvalidOperationException($"Error creating ServiceAccountCredential");
      }

      var initializer = new ServiceAccountCredential.Initializer(serviceAccountCredential.Id, serviceAccountCredential.TokenServerUrl)
      {
        User = serviceAccountCredential.User,
        AccessMethod = serviceAccountCredential.AccessMethod,
        Clock = serviceAccountCredential.Clock,
        Key = serviceAccountCredential.Key,
        Scopes = serviceAccountCredential.Scopes,
        HttpClientFactory = new HttpClientFactory()
      };

      return new ServiceAccountCredential(initializer);
    }

    public async Task<string> CreateAccessTokenAsync(CancellationToken cancellationToken)
    {
      var token = await _credential.GetAccessTokenForRequestAsync();

      var auth = GoogleCredential.FromFile(credentials).CreateScoped(SCOPES);

      var token2 = await auth.UnderlyingCredential.GetAccessTokenForRequestAsync();

      if (token == null)
      {
        throw new InvalidOperationException("Failed to get access token for request");
      }

      return token2;
    }
  }
}
