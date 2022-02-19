using Application.Infrastructure.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Competitions.Commands;

public class CreateCompetitionHandler : IRequestHandler<CreateCompetition>
{
    private readonly ICompetitionDbContext _db;

    public CreateCompetitionHandler(ICompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task<Unit> Handle(CreateCompetition command, CancellationToken cancellationToken)
    {
        var competition = Competition.Create(command.Id, command.Name);

        _db.Competitions.Add(competition);
        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}