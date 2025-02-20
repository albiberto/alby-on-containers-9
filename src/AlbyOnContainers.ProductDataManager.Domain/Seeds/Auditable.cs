namespace AlbyOnContainers.ProductDataManager.Domain.Seeds;

public abstract class Auditable(string? createdBy = null, DateTime? createdAt = null, string? updatedBy = null, DateTime? updatedAt = null)
{
    public string CreatedBy { get; private set; } = createdBy ?? string.Empty;
    public DateTime CreatedAt { get; private set; } = createdAt ?? DateTime.UtcNow;
    public string? UpdatedBy { get; private set; } = updatedBy ?? string.Empty;
    public DateTime? UpdatedAt { get; private set; } = updatedAt ?? DateTime.UtcNow;

    public void Create(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string updatedBy)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.UtcNow;
    }
}