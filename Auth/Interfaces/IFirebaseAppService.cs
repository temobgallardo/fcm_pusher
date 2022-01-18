using FirebasePusher.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace FirebasePusher.Auth.Interfaces
{
  public interface IFirebaseAppService
  {
    FirebaseApp App { get; }

    Task<string> SendMessage(IPushMessage message);

    Task<BatchResponse> SendAllMessages(List<IPushMessage> messages);
  }
}
