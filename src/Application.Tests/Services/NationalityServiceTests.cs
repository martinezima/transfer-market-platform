using TransferMarketPlatform.Application.Services;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Tests.Services;

public class NationalityServiceTests
{
    private readonly NationalityService _service;

    public NationalityServiceTests()
    {
        _service = new NationalityService();
    }

    [Fact]
    public void GetNationalities_ShouldReturnAllCountries()
    {
        // Act
        var result = _service.GetNationalities();

        // Assert
        var expectedCount = Enum.GetValues(typeof(Country)).Length;
        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public void GetNationalities_ShouldReturnUniqueIds()
    {
        // Act
        var result = _service.GetNationalities();

        // Assert
        var distinctIds = result.Select(n => n.Id).Distinct().Count();
        Assert.Equal(result.Count, distinctIds);
    }

    [Fact]
    public void GetNationalities_ShouldReturnNonNullCountryNames()
    {
        // Act
        var result = _service.GetNationalities();

        // Assert
        Assert.All(result, n => Assert.NotNull(n.CountryName));
        Assert.All(result, n => Assert.NotEmpty(n.CountryName));
    }
}
