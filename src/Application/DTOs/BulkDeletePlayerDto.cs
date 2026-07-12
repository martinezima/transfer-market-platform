using System.ComponentModel.DataAnnotations;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.DTOs;

public class BulkDeletePlayerDto
{
    public List<Guid> PlayerIds { get; set; } = new List<Guid>();
}
