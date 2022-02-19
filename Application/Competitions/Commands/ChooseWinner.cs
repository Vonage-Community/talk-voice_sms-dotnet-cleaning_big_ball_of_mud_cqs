using MediatR;

namespace Application.Competitions.Commands;

public class ChooseWinner : IRequest
{
    public Guid CompetitionId { get;init; }

    public ChooseWinner(Guid competitionId)
    {
        CompetitionId = competitionId;
    }
}