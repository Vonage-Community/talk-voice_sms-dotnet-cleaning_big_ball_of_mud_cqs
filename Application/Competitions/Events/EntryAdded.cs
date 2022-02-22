using MediatR;

namespace Application.Competitions.Events;

public class EntryAdded : INotification
{
    public string TelephoneNumber { get; }
    public string Name { get; }

    public EntryAdded(string telephoneNumber, string name)
    {
        TelephoneNumber = telephoneNumber;
        Name = name;
    }
}
