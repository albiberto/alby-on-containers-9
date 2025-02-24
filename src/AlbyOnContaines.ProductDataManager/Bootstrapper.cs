using AlbyOnContainers.ProductDataManager.Infrastructure;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContaines.ProductDataManager.Services;
using Microsoft.EntityFrameworkCore;

namespace AlbyOnContaines.ProductDataManager;

public static class Bootstrapper
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<CategoryDialogService>();
        
        builder.AddInfrastructure();
    }

    static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ProductContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("ProductConnection");
            options.UseNpgsql(connectionString);
        });
        
        builder.Services.Scan(scan => scan
                                  .FromAssemblyOf<ProductContext>()
                                  .AddClasses(classes => classes.InExactNamespaceOf<CategoryRepository>())
                                  .AsSelf()
                                  .WithScopedLifetime());
    }
}

