using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;

public class DescriptionRepository(ProductContext context) : RepositoryBase<DescriptionType>(context)
{
    public Task<int> CountAsync() => context.DescriptionTypes.CountAsync();
}
