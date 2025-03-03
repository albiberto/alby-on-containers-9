using AlbyOnContainers.ProductDataManager.Domain.Seeds;

namespace AlbyOnContainers.ProductDataManager.Domain;

public class DescriptionType(string name, string description, bool enabled = true, Guid id = default) : Entity(id, enabled)
{
    public DescriptionType() : this(string.Empty, string.Empty)
    {
    }
    
    string _name = name;
    public string Name { get => _name; set => _name = value.Trim(); }
    
    string _description = description;
    public string Description { get => _description; set => _description = value.Trim(); }

    readonly List<DescriptionValue> _values = [];
    public ICollection<DescriptionValue> Values => _values;
    
    readonly List<Category> _categories = [];
    public ICollection<Category> Categories => _categories;
}