using AlbyOnContainers.ProductDataManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;

public class CategoryRepository(ProductContext context): ICategoryRepository
{
    public IAsyncEnumerable<Category> GetAllChildren(Guid? parentId = null) =>
        context.Categories
            .Where(c => c.ParentId == parentId)
            .Include(c => c.Children)
            .AsAsyncEnumerable();
}
