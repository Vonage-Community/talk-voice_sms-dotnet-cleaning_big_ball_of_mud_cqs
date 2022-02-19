using System.Reflection;
using Application.Competitions.Commands;
using Application.Infrastructure.Mappings;
using Application.Infrastructure.Persistence;
using Application.Infrastructure.Validation;
using FluentValidation;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebOld.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Database Context
var connectionString = builder.Configuration.GetConnectionString("SQLDBConnection");
builder.Services.AddDbContext<CompetitionDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly(typeof(CompetitionDbContext).Assembly.FullName)));
builder.Services.AddScoped<ICompetitionDbContext>(provider => provider.GetRequiredService<CompetitionDbContext>());


// Filters
builder.Services.AddTransient<TransactionPageFilter>();

// Razor Page
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<TransactionPageFilter>();
    });

// Application
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
builder.Services.AddMediatR(typeof(StartCompetition));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddAutoMapper(c =>
{
    c.AddProfile<CompetitionMappingProfile>();
    c.AddProfile<EntryMappingProfile>();
});


var app = builder.Build();
app.MigrateDb();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();
