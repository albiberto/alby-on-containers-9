using AlbyOnContainers.ProductDataManager.Domain;

namespace AlbyOnContainers.ProductDataManager.Commands;

public class CategoryCommands(ICategoryRepository repository)
{
    public async Task CreateRootCategoryAsync(Category category)
    {
        await repository.CreateAsync(category);
        await repository.SaveChangesAsync();
    }
    
    public Task UpdateRootCategoryAsync() => repository.SaveChangesAsync();
}