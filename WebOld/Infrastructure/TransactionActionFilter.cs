using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebOld.Infrastructure;

public class TransactionPageFilter : IAsyncPageFilter
{
    private readonly CompetitionDbContext _db;


    public TransactionPageFilter(CompetitionDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync();
        var executedContext = await next.Invoke();
        if (executedContext.Exception == null)
        {
            await transaction.CommitAsync();
        }
        else
        {
            await transaction.RollbackAsync();
        }
    }
}