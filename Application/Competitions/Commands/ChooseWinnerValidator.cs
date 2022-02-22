using Application.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Commands;

public class ChooseWinnerValidator : AbstractValidator<ChooseWinner>
{
    private readonly ICompetitionDbContext _db;

    public ChooseWinnerValidator(ICompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));

        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithMessage("Competition Id is required.")
            .MustAsync(ExistAndIsLive).WithMessage("Competition either does not exist or is not live.");
    }

    private async Task<bool> ExistAndIsLive(ChooseWinner command, Guid competitionId, CancellationToken cancellationToken)
    {
        return await _db.Competitions
            .AnyAsync(x => x.Id == command.CompetitionId && x.IsLive, cancellationToken);
    }
}