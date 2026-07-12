using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Interfaces;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Services;

public class NationalityService : INationalityService
{
    // Cache the result since it never changes
    private static readonly List<NationalityDto> _cachedNationalities;

    static NationalityService()
    {
        _cachedNationalities = Enum.GetValues(typeof(Country))
            .Cast<Country>()
            .Select(nationality =>
            {
                var field = nationality.GetType().GetField(nationality.ToString());
                var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

                return new NationalityDto
                {
                    Id = (int)nationality,
                    CountryName = displayAttribute?.Name ?? nationality.ToString(),
                };
            })
            .ToList();
    }

    public List<NationalityDto> GetNationalities()
    {
        return _cachedNationalities.ToList(); // Return a copy to prevent mutation
    }
}
