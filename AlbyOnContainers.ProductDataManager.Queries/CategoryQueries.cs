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

    public async IAsyncEnumerable<TreeItemData<Category>> GetAllHierarchy()
    {
        var categories = await repository.GetAll().OrderBy(c => c.ParentId != null).ToListAsync();
        var lookup = categories.ToLookup(c => c.ParentId);
    
        async IAsyncEnumerable<TreeItemData<Category>> BuildTree(Guid? parentId)
        {
            foreach (var category in lookup[parentId].OrderBy(c => c.Name))
            {
                var children = await BuildTree(category.Id).ToListAsync();
                var item = new TreeItemData<Category>
                {
                    Text = category.Name,
                    Value = category,
                    Expandable = children.Any(),
                    Children = children.ToList()
                };
                yield return item;
            }
        }
    
        await foreach (var item in BuildTree(null))
        {
            yield return item;
        }
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