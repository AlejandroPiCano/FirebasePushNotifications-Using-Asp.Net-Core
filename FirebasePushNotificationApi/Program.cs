using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault(),
});

//FirebaseApp.Create();

app.MapGet("/SingleDeviceMessage", async () => {
    SingleDeviceMessage(); 
});

app.MapGet("/MulticastDeiveMessage", async () => {
    MulticastMessage();
});

async void SingleDeviceMessage()
{
    // This registration token comes from the client FCM SDKs.
    var registrationToken = "MYTOKENID";

    // See documentation on defining a message payload.
    var message = new Message()
    {
        Data = new Dictionary<string, string>()
    {
        { "score", "850" },
        { "time", "2:45" },
    },
        Token = registrationToken,
    };

    // Send a message to the device corresponding to the provided
    // registration token.
    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    // Response is a message ID string.
    Console.WriteLine("Successfully sent message: " + response);
}

async void MulticastMessage()
{

      // [START send_multicast_error]
       // These registration tokens come from the client FCM SDKs.
           var registrationTokens = new List<string>()
           {
                "MYTOKENID1",
                // ...
                "MYTOKENID2",
           };
    var message = new MulticastMessage()
    {
        Tokens = registrationTokens,
        Data = new Dictionary<string, string>()
                {
                    { "score", "850" },
                    { "time", "2:45" },
                },
    };

    var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
    if (response.FailureCount > 0)
    {
        var failedTokens = new List<string>();
        for (var i = 0; i < response.Responses.Count; i++)
        {
            if (!response.Responses[i].IsSuccess)
            {
                // The order of responses corresponds to the order of the registration tokens.
                failedTokens.Add(registrationTokens[i]);
            }
        }

        Console.WriteLine($"List of tokens that caused failures: {failedTokens}");
    }

    // [END send_multicast_error]

}

app.Run();
