using FluentValidation.TestHelper;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Validators;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Tests.Validators;

public class CreatePlayerValidatorTests
{
    private readonly CreatePlayerValidator _validator;

    public CreatePlayerValidatorTests()
    {
        _validator = new CreatePlayerValidator();
    }

    [Fact]
    public void Validate_ValidPlayer_ReturnsNoErrors()
    {
        var playerDto = new CreatePlayerDto
        {
            Name = "Lionel Messi",
            Nationality = Country.Argentina,
            Age = 39,
            CurrentClub = "Inter Miami",
            // Making test fail
            // TransferCost = 600000000m,
            TransferCost = 50000000m,
        };

        var result = _validator.Validate(playerDto);

        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_MissingName_ReturnsOneError()
    {
        var playerDto = new CreatePlayerDto
        {
            // Name = "Lionel Messi",
            Nationality = Country.Argentina,
            Age = 39,
            CurrentClub = "Inter Miami",
            TransferCost = 50000000m,
        };

        var result = _validator.Validate(playerDto);

        Assert.Single(result.Errors);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(CreatePlayerDto.Name));
    }
}
