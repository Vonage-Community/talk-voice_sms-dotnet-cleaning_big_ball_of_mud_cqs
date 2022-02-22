using Application.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class AddEntryToCompetitionValidator : AbstractValidator<AddEntryToCompetition>
{
    private readonly ICompetitionDbContext _db;

    public AddEntryToCompetitionValidator(ICompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithMessage("Competition Id is required.")
            .MustAsync(ExistAndIsLive).WithMessage("Competition either does not exist or is not live.");

        RuleFor(c => c.TelephoneNumber)
            .NotEmpty().WithMessage("TelephoneNumber is required.")
            .MustAsync(BeUniqueTelephoneNumber).WithMessage("A telephone can only enter once.");

    }

    private async Task<bool> ExistAndIsLive(AddEntryToCompetition command, Guid competitionId, CancellationToken cancellationToken)
    {
        return await _db.Competitions
            .AnyAsync(x => x.Id == command.CompetitionId && x.IsLive, cancellationToken);
    }

    private async Task<bool> BeUniqueTelephoneNumber(AddEntryToCompetition command, string telephoneNumber, CancellationToken cancellationToken)
    {
        var competition = await _db.Competitions
            .FirstOrDefaultAsync(x => x.Id == command.CompetitionId, cancellationToken);

        return competition != null 
               && competition.Entries.All(x => x.TelephoneNumber != command.TelephoneNumber);
    }
}
