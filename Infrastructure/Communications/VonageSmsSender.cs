using Application.Infrastructure.Communications;
using Microsoft.Extensions.Options;
using Vonage;
using Vonage.Request;

namespace Infrastructure.Communications
{
    public class VonageSmsSender : ISmsSender
    {
        private readonly VonageSettings _settings;
        
        public VonageSmsSender(IOptions<VonageSettings> settings)
        {
            _settings = settings.Value;
        }


        public async Task SendSms(string toNumber, string message)
        {
            var credentials = Credentials.FromApiKeyAndSecret(_settings.ApiKey, _settings.ApiSecret);
            var vonage = new VonageClient(credentials);
            var response = await vonage.SmsClient.SendAnSmsAsync("Vonage Compeition", toNumber, message);
        }
    }
}
