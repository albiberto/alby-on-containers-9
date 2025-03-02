using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Specifications;

public sealed class DescriptionSpecification : Specification<DescriptionType>
{
    public DescriptionSpecification(GridState<DescriptionType> state, string? searchString = null)
    {
        Query
            .Include(type => type.Values)
            .Include(type => type.Categories);

        if (!string.IsNullOrEmpty(searchString))
            Query.Where(type =>
                            type.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) ||
                            type.Description.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) ||
                            type.Values.Any(v =>
                                                v.Value.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) ||
                                                v.Description.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
                       );
        
        ApplySorting(Query, state);

        Query
            .Skip(state.Page * state.PageSize)
            .Take(state.PageSize);
    }

    static void ApplySorting<T>(ISpecificationBuilder<T> query, GridState<T> state)
    {
        if (state.SortDefinitions.Count == 0) return;

        IOrderedSpecificationBuilder<T>? orderedQuery = null;

        foreach (var (definition, index) in state.SortDefinitions.Select((definition, index) => (definition, index)))
        {
            if (index == 0)
            {
                orderedQuery = definition.Descending
                                   ? query.OrderByDescending(e => EF.Property<object>(e, definition.SortBy))
                                   : query.OrderBy(e => EF.Property<object>(e, definition.SortBy));
            }
            else
            {
                orderedQuery = definition.Descending
                                   ? orderedQuery!.ThenByDescending(e => EF.Property<object>(e, definition.SortBy))
                                   : orderedQuery!.ThenBy(e => EF.Property<object>(e, definition.SortBy));
            }
        }
    }
}