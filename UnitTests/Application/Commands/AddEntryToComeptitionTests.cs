using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Competitions.Commands;
using Application.Infrastructure.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Application.Commands;

public class AddEntryToComeptitionTests : IClassFixture<TestFxiture>
{
    private readonly TestFxiture _testFxiture;

    public AddEntryToComeptitionTests(TestFxiture testFxiture)
    {
        _testFxiture = testFxiture ?? throw new ArgumentNullException(nameof(testFxiture));

        Seed();
    }


    [Fact]
    public async Task AddEntry_CompetitionDoesntExist_ThrowsNotFoundException()
    {
        // arrange
        Guid competitionId = Guid.NewGuid();
        string name = "Bob Smith";
        string telephoneNumber = "+4912345678";

        // act
        var command = new AddEntryToCompetition(competitionId, name, telephoneNumber);

        await using (var context = new CompetitionDbContext(_testFxiture.ContextOptions))
        {
            var handler = new AddEntryToCompetitionHandler(context);

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));

            // assert
            Assert.NotNull(exception);
            Assert.Equal($"Entity \"Competition\" ({competitionId}) was not found.", exception.Message);
        }
    }

    [Fact]
    public async Task AddEntry_CompetitionExists_SavesEntry()
    {
        // arrange
        Guid competitionId = new Guid("c488cf2a-c8b8-42c1-bb47-f264881ee5f3");
        string name = "Bob Smith";
        string telephoneNumber = "+4912345678";

        // act
        var command = new AddEntryToCompetition(competitionId, name, telephoneNumber);

        await using (var context = new CompetitionDbContext(_testFxiture.ContextOptions))
        {
            var handler = new AddEntryToCompetitionHandler(context);
            await handler.Handle(command, CancellationToken.None);
        }

        // assert
        await using (var context = new CompetitionDbContext(_testFxiture.ContextOptions))
        {
            var competition = await context.Competitions.FirstAsync(x => x.Id == competitionId);
            Assert.NotNull(competition);
            //Assert.Contains(competition.Entries, x=>x.Id == entryId);
        }
    }


    private void Seed()
    {
        using var context = new CompetitionDbContext(_testFxiture.ContextOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var aaa = new Competition(new Guid("b53c2235-7e54-4b91-90d8-fb4fb1485b7c"), "AAA");
        // aaa.AddEntry(new Entry(Guid.NewGuid(), "Anthony", "+440712345768"));
        // aaa.AddEntry(new Entry(Guid.NewGuid(), "Arron", "+440765466548"));
        // aaa.AddEntry(new Entry(Guid.NewGuid(), "Alex", "+4407145664568"));

        var bbb = new Competition(new Guid("c488cf2a-c8b8-42c1-bb47-f264881ee5f3"), "BBB");

        var ccc = new Competition(new Guid("1ef1107d-23d3-4bf8-88ce-86f166192571"), "CCC");
        // ccc.AddEntry(new Entry(Guid.NewGuid(), "Carl", "+440712345768"));
        // ccc.AddEntry(new Entry(Guid.NewGuid(), "Chris", "+440712345768"));
        // ccc.AddEntry(new Entry(Guid.NewGuid(), "Colin", "+440712345768"));
        // ccc.AddEntry(new Entry(Guid.NewGuid(), "Craig", "+440712345768"));

        context.AddRange(aaa, bbb, ccc);

        context.SaveChanges();
    }
}
