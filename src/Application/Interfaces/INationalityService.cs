using TransferMarketPlatform.Application.DTOs;

namespace TransferMarketPlatform.Application.Interfaces;

public interface INationalityService
{
    List<NationalityDto> GetNationalities();
}
