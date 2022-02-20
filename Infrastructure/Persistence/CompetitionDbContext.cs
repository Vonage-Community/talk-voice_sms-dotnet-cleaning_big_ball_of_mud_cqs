using Application.Infrastructure.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class CompetitionDbContext : DbContext, ICompetitionDbContext
{
    public virtual DbSet<Competition> Competitions { get; set; }

    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            entity.Property(e => e.TelephoneNumber)
                .IsRequired(true)
                .HasMaxLength(20);

            entity.OwnsMany(e => e.Entries);
            
            entity.OwnsOne(e => e.Winner, w=>
            {
            }).Navigation(p=>p.Winner).IsRequired(false);
        });
    }
}