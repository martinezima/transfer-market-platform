using FluentValidation;
using TransferMarketPlatform.Application.Interfaces;
using TransferMarketPlatform.Application.Mappings;
using TransferMarketPlatform.Application.Services;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Infrastructure.Data;
using TransferMarketPlatform.Infrastructure.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// Register FluentValidation validators from the assembly containing CreatePlayerValidator
builder.Services.AddValidatorsFromAssembly(typeof(CreatePlayerValidator).Assembly);

//Register Application Services
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Register Infrastructure services
var useSqliteDemo = builder.Configuration.GetValue<bool>("UseSqliteDemo");
builder.Services.AddInfrastructure(builder.Configuration, useSqliteDemo);

// Register AutoMapper profiles from the assembly containing PlayerProfile
builder.Services.AddAutoMapper(typeof(PlayerProfile).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    if (File.Exists("transferMarketDemo.db"))
    {
        File.Delete("transferMarketDemo.db");
    }
    var context = scope.ServiceProvider.GetRequiredService<TransferMarketDbContext>();
    await context.Database.EnsureCreatedAsync();
    await DatabaseSeeder.SeedPlayers(context);
}

app.UseHttpsRedirection();

app.Run();
