using Application.Competitions.Events;
using Application.Infrastructure.Persistence;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class AddEntryToCompetitionHandler : AsyncRequestHandler<AddEntryToCompetition>
{
    private readonly ICompetitionDbContext _db;
    private readonly IPublisher _publisher;

    public AddEntryToCompetitionHandler(ICompetitionDbContext db, IPublisher publisher)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    protected override async Task Handle(AddEntryToCompetition command, CancellationToken cancellationToken)
    {
        // get entity
        var compeition = await _db.Competitions.FirstOrDefaultAsync(x => x.Id == command.CompetitionId, cancellationToken);

        // mutation
        var entry = new Entry(command.Name, command.TelephoneNumber);
        compeition.AddEntry(entry);

        await _db.SaveChangesAsync(cancellationToken);

        // send event
        var entryAdded = new EntryAdded(command.TelephoneNumber, command.Name);
        await _publisher.Publish(entryAdded);
    }
}