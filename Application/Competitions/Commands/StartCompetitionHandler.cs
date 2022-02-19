using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class StartCompetitionHandler : IRequestHandler<StartCompetition>
{
    private readonly ICompetitionDbContext _db;

    public StartCompetitionHandler(ICompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Unit> Handle(StartCompetition command, CancellationToken cancellationToken)
    {
        var competition = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == command.CompetitionId, cancellationToken);
        if (competition == null)
        {
            throw new NotFoundException(nameof(Competition), command.CompetitionId);
        }

        competition.Start();
        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}