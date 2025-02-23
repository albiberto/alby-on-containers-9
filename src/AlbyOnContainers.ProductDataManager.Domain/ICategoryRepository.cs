namespace AlbyOnContainers.ProductDataManager.Domain;

public interface ICategoryRepository : IUnitOfWork
{
    IAsyncEnumerable<Category> GetAll();

    IAsyncEnumerable<Category> GetAllChildren(Guid? parentId = null);

    Task CreateAsync(Category category);
    Task<int> SaveChangesAsync();
}