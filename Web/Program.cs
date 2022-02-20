using Application.Competitions.Commands;
using Application.Infrastructure;
using Application.Infrastructure.Communications;
using Application.Infrastructure.Mappings;
using Application.Infrastructure.Persistence;
using Application.Infrastructure.Validation;
using FluentValidation;
using Infrastructure.Communications;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

// Database Context
var connectionString = builder.Configuration.GetConnectionString("SQLDBConnection");
builder.Services.AddDbContextFactory<CompetitionDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly(typeof(CompetitionDbContext).Assembly.FullName)));


builder.Services.AddTransient<ICompetitionDbContext>(provider =>
{
    var factory =provider.GetRequiredService<IDbContextFactory<CompetitionDbContext>>();
    return factory.CreateDbContext();
});

// Application
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
builder.Services.AddMediatR(typeof(StartCompetition));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddAutoMapper(c =>
{
    c.AddProfile<CompetitionMappingProfile>();
    c.AddProfile<EntryMappingProfile>();
});

// Communications
builder.Services.Configure<VonageSettings>(builder.Configuration.GetSection("VonageSettings"));
builder.Services.AddTransient<ISmsSender, VonageSmsSender>();
builder.Services.AddTransient<IPhoneCaller, VonagePhoneCaller>();


var app = builder.Build();
app.MigrateDb();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMvcWithDefaultRoute();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
