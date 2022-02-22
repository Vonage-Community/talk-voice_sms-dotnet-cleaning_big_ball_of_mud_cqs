using Application.Competitions.Events;
using Application.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class ChooseWinnerHandler : AsyncRequestHandler<ChooseWinner>
{
    private readonly ICompetitionDbContext _db;
    private readonly IPublisher _publisher;

    public ChooseWinnerHandler(ICompetitionDbContext db, IPublisher publisher)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    protected override async Task Handle(ChooseWinner command, CancellationToken cancellationToken)
    {
        // get and check we have competition
        var competition = await _db.Competitions
            .FirstOrDefaultAsync(x => x.Id == command.CompetitionId, cancellationToken);

        // choose winner and persist
        competition.ChooseWinner();
        await _db.SaveChangesAsync(cancellationToken);

        // send event
        var winnerChosen = new WinnerChosen(competition.Name,
            competition.Winner.Name, competition.Winner.TelephoneNumber);

        await _publisher.Publish(winnerChosen, cancellationToken);
    }
}