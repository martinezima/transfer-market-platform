using TransferMarketPlatform.Domain.Common;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Domain.Entities;

public class Player : AuditableEntity
{
    public Player() { }

    public Player(
        string? name,
        Country nationality,
        int age,
        string? currentClub,
        decimal transferCost
    )
    {
        Name = name ?? string.Empty;
        Nationality = nationality;
        Age = age;
        CurrentClub = currentClub ?? string.Empty;
        TransferCost = transferCost;
    }

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Country Nationality { get; set; }

    public int Age { get; set; }

    public string CurrentClub { get; set; } = string.Empty;

    public decimal TransferCost { get; set; }
}
