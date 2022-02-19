using MediatR;

namespace Application.Competitions.Commands;

public class AddEntryToCompetition : IRequest
{
    public Guid CompetitionId { get; init; }
    public string Name { get; init; }
    public string TelephoneNumber { get; init; }

    public AddEntryToCompetition(Guid competitionId, string name, string telephoneNumber)
    {
        CompetitionId = competitionId;
        Name = name;
        TelephoneNumber = telephoneNumber;
    }
}