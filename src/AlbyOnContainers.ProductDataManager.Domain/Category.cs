using AlbyOnContainers.ProductDataManager.Domain.Seeds;

namespace AlbyOnContainers.ProductDataManager.Domain;

public class Category(string name, string description, Guid? parentId = null, bool enabled = true, Guid id = default)
    : Entity(id, enabled)
{
    public Category() : this(string.Empty, string.Empty) { }
    
    string _name = name;
    public string Name { get => _name; set => _name = value.Trim(); }

    string _description = description;
    public string Description { get => _description; set => _description = value.Trim(); }
    
    public Guid? ParentId { get; set; } = parentId;
    public Category? Parent { get; set; }

    readonly List<Category> _children = [];
    public bool HasChildren => _children.Count > 0;
    public ICollection<Category> Children => _children;
    
    readonly List<DescriptionType> _descriptionTypes = [];
    public ICollection<DescriptionType> DescriptionTypes => _descriptionTypes;

}