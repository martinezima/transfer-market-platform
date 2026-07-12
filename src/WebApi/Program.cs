using FluentValidation;
using Swashbuckle.AspNetCore.SwaggerGen;
using TransferMarketPlatform.Application.Interfaces;
using TransferMarketPlatform.Application.Mappings;
using TransferMarketPlatform.Application.Services;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Infrastructure.Data;
using TransferMarketPlatform.Infrastructure.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAnyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register FluentValidation validators from the assembly containing CreatePlayerValidator
builder.Services.AddValidatorsFromAssembly(typeof(CreatePlayerValidator).Assembly);

//Register Application Services
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<INationalityService, NationalityService>();

// Register Infrastructure services
var useSqliteDemo = builder.Configuration.GetValue<bool>("UseSqliteDemo");
builder.Services.AddInfrastructure(builder.Configuration, useSqliteDemo);

// Register AutoMapper profiles from the assembly containing PlayerProfile
builder.Services.AddAutoMapper(typeof(PlayerProfile).Assembly);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
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
app.MapControllers();
app.Run();
