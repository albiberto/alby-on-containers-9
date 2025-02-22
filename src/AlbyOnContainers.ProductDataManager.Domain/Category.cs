using System.Collections.Frozen;
using AlbyOnContainers.ProductDataManager.Domain.Seeds;

namespace AlbyOnContainers.ProductDataManager.Domain;

public class Category(string name, string description, Guid? parentId = null, bool enabled = false, Guid id = default)
    : Entity(id, enabled)
{
    public Category() : this(string.Empty, string.Empty) { }

    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public Guid? ParentId { get; private set; } = parentId;
    public Category? Parent { get; private set; }

    List<Category> _children = [];
    public ICollection<Category> Children => _children;

    public bool HasChildren => _children.Count > 0;

    public void AddCategory(Category child) => _children.Add(child);
}