using MediatR;

namespace Application.Competitions.Commands;

public class StartCompetition : IRequest
{
    public Guid CompetitionId { get; init; }

    public StartCompetition(Guid competitionId)
    {
        CompetitionId = competitionId;
    }
}