using TransferMarketPlatform.Domain.Common;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Domain.Entities;

public class Player : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Country Nationality { get; set; }

    public int Age { get; set; }

    public string CurrentClub { get; set; } = string.Empty;

    public decimal TransferCost { get; set; }
}
