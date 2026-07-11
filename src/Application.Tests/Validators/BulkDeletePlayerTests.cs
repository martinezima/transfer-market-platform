using FluentValidation.TestHelper;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Validators;
using Xunit;

namespace TransferMarketPlatform.Application.Tests.Validators;

public class BulkDeletePlayerTests
{
    private readonly BulkDeletePlayerValidator _validator;

    public BulkDeletePlayerTests()
    {
        _validator = new BulkDeletePlayerValidator();
    }

    [Fact]
    public void Validate_ValidIds_ReturnsNoErrors()
    {
        var dto = new BulkDeletePlayerDto
        {
            PlayerIds = new List<int> { 1, 2, 3 },
        };

        var result = _validator.Validate(dto);

        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_InvalidIds_ReturnsTwoErrors()
    {
        var dto = new BulkDeletePlayerDto
        {
            // contains an invalid id (0) and a duplicate (2)
            PlayerIds = new List<int> { 1, 0, 2, 2 },
        };

        var result = _validator.Validate(dto);

        Assert.True(result.Errors.Count == 2, "Expected two validation errors.");
        Assert.Contains(
            result.Errors,
            e => e.PropertyName == nameof(BulkDeletePlayerDto.PlayerIds)
        );
    }
}
