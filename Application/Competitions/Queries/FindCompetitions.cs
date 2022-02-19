using Application.Competitions.Models;
using MediatR;

namespace Application.Competitions.Queries
{
    public class FindCompetitions : IRequest<IEnumerable<CompetitionModel>>
    {
        public int Page { get; }
        public int PageSize { get; }

        public FindCompetitions(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
