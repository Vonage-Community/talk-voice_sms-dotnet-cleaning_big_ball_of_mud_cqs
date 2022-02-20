using Application.Infrastructure.Communications;
using MediatR;

namespace Application.Competitions.Events;

public class WinnerChosenVoiceMessageHandler : INotificationHandler<WinnerChosen>
{
    private readonly IPhoneCaller _phoneCaller;

    public WinnerChosenVoiceMessageHandler(IPhoneCaller phoneCaller)
    {
        _phoneCaller = phoneCaller;
    }

    public async Task Handle(WinnerChosen notification, CancellationToken cancellationToken)
    {
        await _phoneCaller.SendVoiceMessage(notification.WinnerTelephoneNumber,
            $"Congratulations, you have won the competition.");
    }
}