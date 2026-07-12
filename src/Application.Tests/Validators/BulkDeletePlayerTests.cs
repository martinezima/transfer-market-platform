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
            PlayerIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
        };

        var result = _validator.Validate(dto);

        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_InvalidIds_ReturnsTwoErrors()
    {
        var repeatedId = Guid.NewGuid();
        var dto = new BulkDeletePlayerDto
        {
            // contains an invalid id (Guid.Empty) and a repeated id (repeatedId)
            PlayerIds = new List<Guid> { repeatedId, Guid.Empty, Guid.NewGuid(), repeatedId },
        };

        var result = _validator.Validate(dto);

        Assert.True(result.Errors.Count == 2, "Expected two validation errors.");
        Assert.Contains(
            result.Errors,
            e => e.PropertyName == nameof(BulkDeletePlayerDto.PlayerIds)
        );
    }
}
