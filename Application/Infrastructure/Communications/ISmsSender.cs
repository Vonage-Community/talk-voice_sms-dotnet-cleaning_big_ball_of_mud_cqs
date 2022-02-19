namespace Application.Infrastructure.Communications
{
    public interface ISmsSender
    {
        Task SendSms(string toNumber, string message);
    }
}
