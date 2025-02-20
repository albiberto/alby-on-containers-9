using AlbyOnContainers.ProductDataManager.Domain.Seeds;

namespace AlbyOnContainers.ProductDataManager.Domain;

public class Category(string name, string description, Guid? parentId = null, bool enabled = false, Guid id = default) : Entity(id, enabled)
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    
    public Guid? ParentId { get; } = parentId;
    public Category? ParentCategory { get; private set; }
}