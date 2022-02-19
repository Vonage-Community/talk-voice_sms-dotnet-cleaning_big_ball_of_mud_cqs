using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Web.Infrastructure;

public static class CompMigrator
{
    public static IApplicationBuilder MigrateDb(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<CompetitionDbContext>();

        db.Database.Migrate();

        return app;
    }
}