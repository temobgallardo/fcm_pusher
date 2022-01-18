using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FirebasePusher.Auth.Interfaces;
using FirebasePusher.Interfaces;
using Google.Apis.Auth.OAuth2;

namespace FirebasePusher.Auth
{
  public class FirebaseAppService : IFirebaseAppService
  {
    private string credentials = "litio7-pushy-firebase-adminsdk-m0uji-1c39f542bc.json";
    private const string MESSAGING_SCOPE = "https://www.googleapis.com/auth/firebase.messaging";
    private string[] SCOPES = new string[] { MESSAGING_SCOPE };
    private FirebaseApp _app;

    public FirebaseApp App => _app;

    public FirebaseAppService()
    {
      _app = FirebaseApp.Create(new AppOptions
      {
        Credential = GoogleCredential.FromFile(credentials)
      });
    }

    public async Task<string> SendMessage(IPushMessage message)
    {
      var fcmMessage = PushMessageToMessage(message);
      return await FirebaseMessaging.DefaultInstance.SendAsync(fcmMessage);
    }

    public async Task<BatchResponse> SendAllMessages(List<IPushMessage> messages)
    {
      var fcmMessages = messages.Select(m => PushMessageToMessage(m));
      return await FirebaseMessaging.DefaultInstance.SendAllAsync(fcmMessages);
    }

    private Message PushMessageToMessage(IPushMessage message)
    {
      const string iosPlatform = "ios";
      const string titleKey = "title";
      const string bodyKey = "body";

      var data = new Dictionary<string, string>(message.Data);

      data.Add(titleKey, message.Title);
      data.Add(bodyKey, message.Body);

      var pushMessage = new Message
      {
        Topic = message.HasTopic ? message.Topic : message.To,
        Notification = new Notification
        {
          Title = message.Title,
          Body = message.Body
        },
        Data = data,
      };

      var platformSpecificMessage = message as IPlatformSpecificPushMessage;

      if (platformSpecificMessage.Platform == iosPlatform)
      {
        pushMessage.Apns = new ApnsConfig
        {
          Aps = new Aps
          {
            ContentAvailable = true,
          }
        };
      }

      return pushMessage;
    }
  }
}
