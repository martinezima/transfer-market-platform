using AutoMapper;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Extensions;
using TransferMarketPlatform.Domain.Entities;

namespace TransferMarketPlatform.Application.Mappings;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>()
            .ForMember(
                dest => dest.Nationality,
                opt => opt.MapFrom(src => src.Nationality.GetDisplayName())
            );

        CreateMap<CreatePlayerDto, Player>()
            .ConstructUsing(src => new Player(
                src.Name,
                src.Nationality,
                src.Age,
                src.CurrentClub,
                src.TransferCost
            ));

        CreateMap<UpdatePlayerDto, Player>()
            .ConstructUsing(src => new Player(
                src.Name,
                src.Nationality,
                src.Age,
                src.CurrentClub,
                src.TransferCost
            ));
    }
}
