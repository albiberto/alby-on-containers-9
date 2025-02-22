using AlbyOnContainers.ProductDataManager.Domain;
using MudBlazor;

namespace AlbyOnContainers.ProductDataManager.Queries;

public class CategoryQueries(ICategoryRepository repository)
{
    public Task<List<TreeItemData<Category>>> GetAllParents() => 
        GetAll(() => repository.GetAllChildren());
    
    public Task<List<TreeItemData<Category>>> GetAllChildren(Guid parentId) => 
        GetAll(() => repository.GetAllChildren(parentId));

    static async Task<List<TreeItemData<Category>>> GetAll(Func<IAsyncEnumerable<Category>> selector) =>
        await selector()
            .Select(category => new TreeItemData<Category>
            {
                Text = category.Name,
                Value = category,
                Expandable = category.HasChildren
            }).ToListAsync();
}