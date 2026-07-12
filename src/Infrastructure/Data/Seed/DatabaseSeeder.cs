using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Infrastructure.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedPlayers(TransferMarketDbContext dbContext)
        {
            var players = new List<Player>
            {
                new()
                {
                    Name = "Cristiano Ronaldo",
                    Age = 39,
                    Nationality = Country.Portugal,
                    CurrentClub = "Al Nassr",
                    TransferCost = 50000000m,
                },
                new()
                {
                    Name = "Lionel Messi",
                    Age = 37,
                    Nationality = Country.Argentina,
                    CurrentClub = "Inter Miami",
                    TransferCost = 45000000m,
                },
                new()
                {
                    Name = "Kylian Mbappé",
                    Age = 25,
                    Nationality = Country.France,
                    CurrentClub = "Real Madrid",
                    TransferCost = 180000000m,
                },
                new()
                {
                    Name = "Florian Wirtz",
                    Age = 21,
                    Nationality = Country.Germany,
                    CurrentClub = "Bayer Leverkusen",
                    TransferCost = 120000000m,
                },
                new()
                {
                    Name = "Jude Bellingham",
                    Age = 21,
                    Nationality = Country.England,
                    CurrentClub = "Real Madrid",
                    TransferCost = 130000000m,
                },
                new()
                {
                    Name = "Pedri",
                    Age = 21,
                    Nationality = Country.Spain,
                    CurrentClub = "Barcelona",
                    TransferCost = 95000000m,
                },
                new()
                {
                    Name = "Gianfranco Zola",
                    Age = 32,
                    Nationality = Country.Italy,
                    CurrentClub = "Retired",
                    TransferCost = 0m,
                },
                new()
                {
                    Name = "Virgil van Dijk",
                    Age = 33,
                    Nationality = Country.Netherlands,
                    CurrentClub = "Liverpool",
                    TransferCost = 85000000m,
                },
                new()
                {
                    Name = "Luis Suárez",
                    Age = 37,
                    Nationality = Country.Uruguay,
                    CurrentClub = "Grêmio",
                    TransferCost = 30000000m,
                },
                new()
                {
                    Name = "Kevin De Bruyne",
                    Age = 33,
                    Nationality = Country.Belgium,
                    CurrentClub = "Manchester City",
                    TransferCost = 75000000m,
                },
                new()
                {
                    Name = "Luka Modrić",
                    Age = 39,
                    Nationality = Country.Croatia,
                    CurrentClub = "Real Madrid",
                    TransferCost = 40000000m,
                },
                new()
                {
                    Name = "Christian Eriksen",
                    Age = 32,
                    Nationality = Country.Denmark,
                    CurrentClub = "Manchester United",
                    TransferCost = 35000000m,
                },
                new()
                {
                    Name = "Takefusa Kubo",
                    Age = 23,
                    Nationality = Country.Japan,
                    CurrentClub = "Real Sociedad",
                    TransferCost = 30000000m,
                },
                new()
                {
                    Name = "Hirving Lozano",
                    Age = 29,
                    Nationality = Country.Mexico,
                    CurrentClub = "San Diego FC",
                    TransferCost = 25000000m,
                },
                new()
                {
                    Name = "Hakim Ziyech",
                    Age = 32,
                    Nationality = Country.Morocco,
                    CurrentClub = "Al Nassr",
                    TransferCost = 22000000m,
                },
                new()
                {
                    Name = "Sadio Mané",
                    Age = 32,
                    Nationality = Country.Senegal,
                    CurrentClub = "Al Nassr",
                    TransferCost = 28000000m,
                },
                new()
                {
                    Name = "Christian Pulisic",
                    Age = 25,
                    Nationality = Country.UnitedStates,
                    CurrentClub = "AC Milan",
                    TransferCost = 30000000m,
                },
                new()
                {
                    Name = "James Rodríguez",
                    Age = 33,
                    Nationality = Country.Colombia,
                    CurrentClub = "Rivers Plate",
                    TransferCost = 20000000m,
                },
                new()
                {
                    Name = "Enner Valencia",
                    Age = 34,
                    Nationality = Country.Ecuador,
                    CurrentClub = "Tiburones Rojos",
                    TransferCost = 15000000m,
                },
                new()
                {
                    Name = "Neymar",
                    Age = 32,
                    Nationality = Country.Brazil,
                    CurrentClub = "Al Hilal",
                    TransferCost = 90000000m,
                },
            };

            dbContext.Players.AddRange(players);
            await dbContext.SaveChangesAsync();
        }
    }
}
