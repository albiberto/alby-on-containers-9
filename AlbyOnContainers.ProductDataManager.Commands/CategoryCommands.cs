using AlbyOnContainers.ProductDataManager.Domain;

namespace AlbyOnContainers.ProductDataManager.Commands;

public class CategoryCommands(ICategoryRepository repository)
{
    public async Task CreateRootCategoryAsync(Category category)
    {
        await repository.CreateAsync(category);
        await repository.SaveChangesAsync();
    }
    
    public async Task CreateChildCategoryAsync(Category parent, Category child)
    {
        parent.AddCategory(child);
        await repository.SaveChangesAsync();
    }
}