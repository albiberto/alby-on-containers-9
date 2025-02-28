using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification;

namespace AlbyOnContaines.ProductDataManager.Specifications;

public sealed class CategoryWithParentSpecification: Specification<Category>
{
    public CategoryWithParentSpecification() => Query.Include(category => category.Parent);
}