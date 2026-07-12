using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.DTOs;

public class PlayerDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NationalityDisplayName { get; set; }

    public int Age { get; set; }

    public string? CurrentClub { get; set; }

    public string TransferCostDisplay { get; set; } = string.Empty;
}
