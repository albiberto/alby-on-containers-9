using AlbyOnContainers.ProductDataManager.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Interceptors;

public class AuditableInterceptor : SaveChangesInterceptor 
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SaveChanges(eventData);

        return ValueTask.FromResult(result);
    }

    static void SaveChanges(DbContextEventData eventData)
    {
        var context = eventData.Context!;

        SaveChanges(context);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context!;
        
        SaveChanges(context);
        
        return result;
    }

    static void SaveChanges(DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<Auditable>())
        {
            switch (entry)
            {
                case { State: EntityState.Added }:
                    entry.Entity.Create("Alberto");
                break;

                case { State: EntityState.Modified }:
                    entry.Entity.Update("Roald");
                break;
            }
        }
    }
}