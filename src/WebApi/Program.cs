using FluentValidation;
using TransferMarketPlatform.Application.Interfaces;
using TransferMarketPlatform.Application.Mappings;
using TransferMarketPlatform.Application.Services;
using TransferMarketPlatform.Application.Validators;

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
}

app.UseHttpsRedirection();

app.Run();
