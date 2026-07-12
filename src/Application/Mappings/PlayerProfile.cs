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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.NationalityDisplayName,
                opt => opt.MapFrom(src => src.Nationality.GetDisplayName())
            )
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.CurrentClub, opt => opt.MapFrom(src => src.CurrentClub))
            .ForMember(
                dest => dest.TransferCostDisplay,
                opt => opt.MapFrom(src => src.TransferCost.ToString("C"))
            );

        CreateMap<CreatePlayerDto, Player>()
            .ConstructUsing(src => new Player(
                src.Id,
                src.Name,
                src.Nationality,
                src.Age,
                src.CurrentClub,
                src.TransferCost
            ))
            .ForAllMembers(opt => opt.Ignore()); // Prevents AutoMapper from validating inherited or unmapped setters

        CreateMap<UpdatePlayerDto, Player>()
            .ConstructUsing(src => new Player(
                src.Id,
                src.Name,
                src.Nationality,
                src.Age,
                src.CurrentClub,
                src.TransferCost
            ))
            .ForAllMembers(opt => opt.Ignore()); // Prevents validation errors for UpdatePlayerDto
    }
}
