using System.ComponentModel.DataAnnotations;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.DTOs;

public class BulkDeletePlayerDto
{
    public List<int> PlayerIds { get; set; } = new List<int>();
}
