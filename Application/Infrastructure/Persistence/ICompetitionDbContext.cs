using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public interface ICompetitionDbContext
    {
        DbSet<Competition> Competitions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
