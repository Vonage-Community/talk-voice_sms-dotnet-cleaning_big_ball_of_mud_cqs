using Application.Competitions.Models;
using Application.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Queries;

public class FindCompetitionsHandler : IRequestHandler<FindCompetitions, IEnumerable<CompetitionModel>>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public FindCompetitionsHandler(ICompetitionDbContext db, IMapper mapper)
    {
        _dbContext = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CompetitionModel>> Handle(FindCompetitions query, CancellationToken cancellationToken)
    {
        var competitions = await _dbContext.Competitions
            .OrderBy(x => x.Name)
            .Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
            .ToListAsync(cancellationToken);

        return _mapper.Map<IEnumerable<CompetitionModel>>(competitions);
    }
}