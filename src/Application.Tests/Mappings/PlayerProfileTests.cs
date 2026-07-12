using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Mappings;
using TransferMarketPlatform.Domain.Entities;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Tests.Mappings;

public class PlayerProfileTests
{
    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<PlayerProfile>();
            },
            NullLoggerFactory.Instance
        );
        configuration.AssertConfigurationIsValid();
        return configuration.CreateMapper();
    }

    [Fact]
    public void Map_Player_To_PlayerDto_Maps_All_Properties_And_Nationality_Display_Name()
    {
        var mapper = CreateMapper();
        var player = new Player(
            Guid.NewGuid(),
            "Cristiano Ronaldo",
            Country.Portugal,
            39,
            "Al Nassr",
            50000000m
        );

        var dto = mapper.Map<PlayerDto>(player);

        Assert.Equal(player.Id, dto.Id);
        Assert.Equal(player.Name, dto.Name);
        Assert.Equal("Portugal", dto.NationalityDisplayName);
        Assert.Equal(player.Age, dto.Age);
        Assert.Equal(player.CurrentClub, dto.CurrentClub);
        Assert.Equal(player.TransferCost.ToString("C"), dto.TransferCostDisplay);
    }

    [Fact]
    public void Map_CreatePlayerDto_To_Player_Maps_All_Properties()
    {
        var mapper = CreateMapper();
        var createDto = new CreatePlayerDto
        {
            Name = "Kylian Mbappé",
            Nationality = Country.France,
            Age = 25,
            CurrentClub = "Real Madrid",
            TransferCost = 180000000m,
        };

        var player = mapper.Map<Player>(createDto);

        Assert.Equal(createDto.Name, player.Name);
        Assert.Equal(createDto.Nationality, player.Nationality);
        Assert.Equal(createDto.Age, player.Age);
        Assert.Equal(createDto.CurrentClub, player.CurrentClub);
        Assert.Equal(createDto.TransferCost, player.TransferCost);
    }

    [Fact]
    public void Map_UpdatePlayerDto_To_Player_Maps_All_Properties()
    {
        var mapper = CreateMapper();
        var updateDto = new UpdatePlayerDto
        {
            Name = "Kylian Mbappé",
            Nationality = Country.France,
            Age = 25,
            CurrentClub = "Real Madrid",
            TransferCost = 180000000m,
        };

        var player = mapper.Map<Player>(updateDto);

        Assert.Equal(updateDto.Name, player.Name);
        Assert.Equal(updateDto.Nationality, player.Nationality);
        Assert.Equal(updateDto.Age, player.Age);
        Assert.Equal(updateDto.CurrentClub, player.CurrentClub);
        Assert.Equal(updateDto.TransferCost, player.TransferCost);
    }
}
