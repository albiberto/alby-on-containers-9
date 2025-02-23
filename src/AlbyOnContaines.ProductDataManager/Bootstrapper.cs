using AlbyOnContainers.ProductDataManager.Commands;
using AlbyOnContainers.ProductDataManager.Infrastructure;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContainers.ProductDataManager.Queries;
using AlbyOnContaines.ProductDataManager.Services;
using Microsoft.EntityFrameworkCore;

namespace AlbyOnContaines.ProductDataManager;

public static class Bootstrapper
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<CategoryDialogService>();
        
        builder.AddInfrastructure();
        builder.AddCqrs();
    }
    
    static void AddCqrs(this IHostApplicationBuilder builder)
    {
        builder.Services.Scan(scan => scan
                                  .FromAssemblyOf<CategoryQueries>()
                                  .AddClasses(classes => classes.InExactNamespaceOf<CategoryQueries>())
                                  .AsSelf()
                                  .WithScopedLifetime());
        
        builder.Services.Scan(scan => scan
                                  .FromAssemblyOf<CategoryCommands>()
                                  .AddClasses(classes => classes.InExactNamespaceOf<CategoryCommands>())
                                  .AsSelf()
                                  .WithScopedLifetime());
        
        builder.Services.Scan(scan => scan
                                  .FromAssemblyOf<ProductContext>()
                                  .AddClasses(classes => classes.InExactNamespaceOf<CategoryRepository>())
                                  .AsImplementedInterfaces()
                                  .WithScopedLifetime());
    }

    static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ProductContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("ProductConnection");
            options.UseNpgsql(connectionString);
        });
    }
}

