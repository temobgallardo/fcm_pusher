// See https://aka.ms/new-console-template for more information
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

var secretFilename = "../../../litio7-pushy-firebase-adminsdk-m0uji-930f3dbdf0.json";
var firebaseApp = FirebaseApp.Create(new AppOptions()
{
  Credential = GoogleCredential.FromFile(secretFilename)
});

var registrationToken = "fUasw_C8TyO1NDeiF3MPTs:APA91bHZJSRzPcsLNYLEXviKPustkAY8gpyomrcFdvH_z47DNf0jOSgXPY-JCyJL2q9kw92U5Jz3yfeu8eB4aT4LZviXfD3ZVKXr6Ett4vWDaTCFfUiuQZVxlMwj_QEL7SqfovzBj3az";

var message = new Message()
{
  Data = new Dictionary<string, string>()
  {
    {"title", "Pushy" },
    {"body", "Pushy is receiving a push from FCM" }
  },
  Token = registrationToken,
};

try
{
  var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
  Console.WriteLine("Successfully sent message: " + response);
}
catch (Exception _)
{
  Console.WriteLine($"Failed to send message: {_.Message}\n{_.StackTrace}");
}