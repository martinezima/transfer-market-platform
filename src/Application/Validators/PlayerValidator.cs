using System.ComponentModel.Design.Serialization;
using FluentValidation;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Domain.Enums;

namespace TransferMarketPlatform.Application.Validators;

public class CreatePlayerValidator : AbstractValidator<CreatePlayerDto>
{
    /// <summary>
    /// Validator for creating new players
    /// </summary>
    public CreatePlayerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Player name is required.")
            .Length(3, 100)
            .WithMessage("Player name must be between 3 and 100 characters.")
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage("Player name can only contain letters, spaces, hyphens, and apostrophes");

        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Country is required");

        RuleFor(x => x.Age)
            .NotEmpty()
            .WithMessage("Age is required")
            .InclusiveBetween(16, 50)
            .WithMessage("Age must be between 16 and 50 years old")
            .Must(BeValidAge)
            .WithMessage("Invalid age value");

        RuleFor(x => x.CurrentClub)
            .NotEmpty()
            .WithMessage("Current club is required")
            .Length(2, 100)
            .WithMessage("Club name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Z0-9\s\-']+$")
            .WithMessage("Club name contains invalid characters");

        RuleFor(x => x.TransferCost)
            .NotEmpty()
            .WithMessage("Transfer cost is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Transfer cost cannot be negative")
            .LessThanOrEqualTo(500000000)
            .WithMessage("Transfer cost cannot exceed 500,000,000");
    }

    private bool BeValidAge(int age)
    {
        return age >= 16 && age <= 50;
    }
}

/// <summary>
/// Validator for updating existing players
/// </summary>
public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerDto>
{
    public UpdatePlayerValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Player ID is required for update");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Player name is required")
            .Length(2, 100)
            .WithMessage("Player name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Z\s\-']+$")
            .WithMessage("Player name can only contain letters, spaces, hyphens, and apostrophes");

        RuleFor(x => x.Nationality).NotEmpty().WithMessage("Country is required");

        RuleFor(x => x.Age)
            .NotEmpty()
            .WithMessage("Age is required")
            .InclusiveBetween(16, 50)
            .WithMessage("Age must be between 16 and 50 years old")
            .Must(BeValidAge)
            .WithMessage("Invalid age value");

        RuleFor(x => x.CurrentClub)
            .NotEmpty()
            .WithMessage("Current club is required")
            .Length(2, 100)
            .WithMessage("Club name must be between 2 and 100 characters")
            .Matches(@"^[a-zA-Z0-9\s\-']+$")
            .WithMessage("Club name contains invalid characters");

        RuleFor(x => x.TransferCost)
            .NotEmpty()
            .WithMessage("Transfer cost is required")
            .GreaterThanOrEqualTo(0)
            .WithMessage("Transfer cost cannot be negative")
            .LessThanOrEqualTo(500000000)
            .WithMessage("Transfer cost cannot exceed 500,000,000");
    }

    private bool BeValidAge(int age)
    {
        return age >= 16 && age <= 50;
    }
}

public class BulkDeletePlayerValidator : AbstractValidator<BulkDeletePlayerDto>
{
    public BulkDeletePlayerValidator()
    {
        RuleFor(x => x.PlayerIds)
            .NotEmpty()
            .NotNull()
            .WithMessage("Player IDs are required for bulk deletion")
            .Must(ids => ids.All(id => id != Guid.Empty))
            .WithMessage("Player IDs cannot contain Guid.Empty")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Duplicate player IDs are not allowed");
    }
}
