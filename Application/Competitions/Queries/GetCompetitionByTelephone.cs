using Application.Competitions.Models;
using Application.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Competitions.Queries;

public class GetCompetitionByTelephone : IRequest<CompetitionModel>
{
    public string Telephone { get; }
    
    public GetCompetitionByTelephone(string telephone)
    {
        Telephone = telephone;
    }
}

public class GetCompetitionByTelephoneHandler : IRequestHandler<GetCompetitionByTelephone, CompetitionModel>
{
    private readonly ICompetitionDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCompetitionByTelephoneHandler(ICompetitionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<CompetitionModel> Handle(GetCompetitionByTelephone query, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions
            .FirstOrDefaultAsync(x => x.TelephoneNumber == query.Telephone, cancellationToken: cancellationToken);

        return _mapper.Map<CompetitionModel>(competition);
    }
}