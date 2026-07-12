using System.ComponentModel.DataAnnotations;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.DTOs;

public class CreatePlayerDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Country Nationality { get; set; }
    public int Age { get; set; }
    public string? CurrentClub { get; set; }
    public decimal TransferCost { get; set; }
}
