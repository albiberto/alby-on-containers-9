namespace AlbyOnContainers.ProductDataManager.Domain;

public interface ICategoryRepository
{ 
    IAsyncEnumerable<Category> GetAllChildren(Guid? parentId = null);
}