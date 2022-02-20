using Application.Infrastructure.Communications;
using MediatR;

namespace Application.Competitions.Events;

public class WinnerChosenSmsHandler : INotificationHandler<WinnerChosen>
{
    private readonly ISmsSender _smsSender;

    public WinnerChosenSmsHandler(ISmsSender smsSender)
    {
        _smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
    }


    public Task Handle(WinnerChosen notification, CancellationToken cancellationToken)
    {
        _smsSender.SendSms(notification.WinnerTelephoneNumber, $"Congratulations you have won the {notification.CompetitionName} competition.");
        
        return Task.CompletedTask;
    }
}