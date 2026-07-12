using TransferMarketPlatform.Domain.Common;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Domain.Entities;

public class Player : AuditableEntity
{
    public Player() { }

    public Player(
        Guid id,
        string? name,
        Country nationality,
        int age,
        string? currentClub,
        decimal transferCost
    )
    {
        Id = id;
        Name = name ?? string.Empty;
        Nationality = nationality;
        Age = age;
        CurrentClub = currentClub ?? string.Empty;
        TransferCost = transferCost;
    }

    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Country Nationality { get; set; }

    public int Age { get; set; }

    public string? CurrentClub { get; set; }

    public decimal TransferCost { get; set; }
}
