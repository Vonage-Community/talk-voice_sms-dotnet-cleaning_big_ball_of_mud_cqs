namespace Application.Infrastructure.Communications;

public interface IPhoneCaller
{
    Task SendVoiceMessage(string toNumber, string message);
}