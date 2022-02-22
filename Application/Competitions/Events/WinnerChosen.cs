using MediatR;

namespace Application.Competitions.Events;

public class WinnerChosen : INotification
{
    public string CompetitionName { get; }
    public string WinnerName { get; }
    public string WinnerTelephoneNumber { get; }

    public WinnerChosen(string competitionName, string winnerName, string winnerTelephoneNumber)
    {
        CompetitionName = competitionName;
        WinnerName = winnerName;
        WinnerTelephoneNumber = winnerTelephoneNumber;
    }
}