namespace AlbyOnContainers.ProductDataManager.Domain.Seeds;

public abstract class Entity(Guid Id, bool enabled, string? createdBy = null, DateTime? createdAt = null, string updatedBy = null, DateTime? updatedAt = null)
    : Auditable(createdBy, createdAt, updatedBy, updatedAt)
{
    public bool Enabled { get; private set; } = enabled;

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;
}