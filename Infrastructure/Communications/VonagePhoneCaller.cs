using Application.Infrastructure.Communications;
using Vonage;
using Vonage.Request;
using Vonage.Voice;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;

namespace Infrastructure.Communications;

public class VonagePhoneCaller : IPhoneCaller
{
    private readonly VonageSettings _settings;

    public VonagePhoneCaller(VonageSettings settings)
    {
        _settings = settings;
    }

    public async Task SendVoiceMessage(string toNumber, string message)
    {
        var credentials = Credentials.FromAppIdAndPrivateKeyPath(_settings.AppId, _settings.PrivateKeyPath);
        // var credentials = Credentials.FromApiKeyAndSecret(_settings.ApiKey, _settings.ApiSecret);
        // credentials.ApplicationId = _settings.AppId;

        var vonage = new VonageClient(credentials);

        
        var callCommand = new CallCommand
        {
            To = new Endpoint[]
            {
                new PhoneEndpoint
                {
                    Number = toNumber
                }
            },
            From = new PhoneEndpoint
            {
                Number = "123456789"
            },
            Ncco = new Ncco(new TalkAction
            {
                Language = "en",
                Style = 2,
                Text = message
            })
        };
        try
        {
            var response = await vonage.VoiceClient.CreateCallAsync(callCommand, credentials);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}