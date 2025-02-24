using AlbyOnContainers.ProductDataManager.Domain;
using MudBlazor;

namespace AlbyOnContainers.ProductDataManager.Queries;

public class CategoryQueries(ICategoryRepository repository)
{
    public IAsyncEnumerable<TreeItemData<Category>> GetAllParents() =>
        GetAll(() => repository.GetAllChildren());

    public IAsyncEnumerable<TreeItemData<Category>> GetAllChildren(Guid parentId) =>
        GetAll(() => repository.GetAllChildren(parentId));

    public async Task<List<(Guid Id, string Name)>> GetAllCategoriesNames()
    {
        var categories = await repository.GetAll()
                             .Select(category => (category.Id, category.Name))
                             .OrderBy(category => category.Name)
                             .ToListAsync();

        categories.Insert(0, (Guid.Empty, "Root"));

        return categories;
    }

    



    static IAsyncEnumerable<TreeItemData<Category>> GetAll(Func<IAsyncEnumerable<Category>> selector) =>
        selector()
            .Select(category => new TreeItemData<Category>
            {
                Text = category.Name,
                Value = category,
                Expandable = category.HasChildren
            }).AsAsyncEnumerable();
}