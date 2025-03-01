using AlbyOnContainers.ProductDataManager.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlbyOnContainers.ProductDataManager.Infrastructure;

public class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<DescriptionType> DescriptionTypes { get; set; }
    public DbSet<DescriptionValue> DescriptionValues { get; set; }
}