using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class AddEntryToCompetitionHandler : IRequestHandler<AddEntryToCompetition>
{
    private readonly ICompetitionDbContext _db;

    public AddEntryToCompetitionHandler(ICompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Unit> Handle(AddEntryToCompetition command, CancellationToken cancellationToken)
    {
        var compeition = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == command.CompetitionId, cancellationToken);
        if (compeition == null)
        {
            throw new NotFoundException(nameof(Competition), command.CompetitionId);
        }

        var entry = new Entry(command.Name, command.TelephoneNumber);
        compeition.AddEntry(entry);

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}