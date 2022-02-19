using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Application.Commands;

public class TestFxiture
{
    public TestFxiture()
    {
        ContextOptions = new DbContextOptionsBuilder<CompetitionDbContext>()
            .UseInMemoryDatabase("CoolComps")
            .Options;
    }

    public DbContextOptions<CompetitionDbContext> ContextOptions { get; }

}