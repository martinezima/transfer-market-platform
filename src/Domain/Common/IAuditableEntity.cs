namespace TransferMarketPlatform.Domain.Common;

/// <summary>
/// Interface for entities that require audit tracking.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    DateTime? LastModifiedAt { get; set; }

    /// <summary>
    /// Gets or sets the user who last modified the entity.
    /// </summary>
    string? LastModifiedBy { get; set; }
}

/// <summary>
/// Base abstract class for auditable entities.
/// </summary>
public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }
}
