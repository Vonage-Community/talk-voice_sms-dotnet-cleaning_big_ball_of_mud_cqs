using MediatR;

namespace Application.Competitions.Commands;

public class ChooseWinner : IRequest
{
    public Guid CompetitionId { get; }

    public ChooseWinner(Guid competitionId)
    {
        CompetitionId = competitionId;
    }
}