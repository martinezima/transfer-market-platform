using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Tests.Validators;

public class UpdatePlayerValidatorTests
{
    private readonly UpdatePlayerValidator _validator;

    public UpdatePlayerValidatorTests()
    {
        _validator = new UpdatePlayerValidator();
    }

    [Fact]
    public void Validate_ValidPlayer_ReturnsNoErrors()
    {
        var playerDto = new UpdatePlayerDto
        {
            Id = 1,
            Name = "Cristiano Ronaldo",
            Nationality = Country.Portugal,
            Age = 38,
            CurrentClub = "Al Nassr",
            TransferCost = 10000000m,
        };

        var result = _validator.Validate(playerDto);

        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_MissingName_ReturnsOneError()
    {
        var playerDto = new UpdatePlayerDto
        {
            Id = 1,
            Name = null,
            Nationality = Country.Portugal,
            Age = 38,
            CurrentClub = "Al Nassr",
            TransferCost = 10000000m,
        };

        var result = _validator.Validate(playerDto);

        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(UpdatePlayerDto.Name));
    }
}
