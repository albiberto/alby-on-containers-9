using AlbyOnContainers.ProductDataManager.Domain;

namespace AlbyOnContainers.ProductDataManager.Commands;

public class CategoryCommands(ICategoryRepository repository)
{
    public async Task CreateRootCategory(Category category)
    {
        await repository.CreateAsync(category);
        await repository.SaveChangesAsync();
    }
}