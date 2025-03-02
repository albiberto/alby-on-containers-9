using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification.EntityFrameworkCore;

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;

public class DescriptionRepository(ProductContext context) : RepositoryBase<DescriptionType>(context);
