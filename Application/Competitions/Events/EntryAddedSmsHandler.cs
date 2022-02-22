using Application.Infrastructure.Communications;
using MediatR;

namespace Application.Competitions.Events;

public class EntryAddedSmsHandler : INotificationHandler<EntryAdded>
{
    private readonly ISmsSender _smsSender;

    public EntryAddedSmsHandler(ISmsSender smsSender)
    {
        _smsSender = smsSender ?? throw new ArgumentNullException(nameof(smsSender));
    }

    public async Task Handle(EntryAdded entryAddedEvent, CancellationToken cancellationToken)
    {
        await _smsSender.SendSms(entryAddedEvent.TelephoneNumber, $"{entryAddedEvent.Name}, thank you for your entry.");
    }
}
