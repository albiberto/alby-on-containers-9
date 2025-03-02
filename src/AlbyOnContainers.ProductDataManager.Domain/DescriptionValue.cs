using AlbyOnContainers.ProductDataManager.Domain.Seeds;

namespace AlbyOnContainers.ProductDataManager.Domain;

public class DescriptionValue(string value, string description, Guid descriptionTypeId, bool enabled = true, Guid id = default): Entity(id, enabled)
{
    string _value = value;
    public string Value { get => _value; set => _value = value.Trim(); }
    
    string _description = description;
    public string Description { get => _description; set => _description = value.Trim(); }

    public Guid DescriptionTypeId { get; private set; } = descriptionTypeId;
    public DescriptionType? DescriptionType { get; set; }

}