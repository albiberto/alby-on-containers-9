using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Specifications;

public sealed class DescriptionGridSpecification : Specification<DescriptionType>
{
    public DescriptionGridSpecification(GridState<DescriptionType> state, string? searchString = null)
    {
        Query
            .Include(type => type.Values)
            .Include(type => type.Categories);

        if (!string.IsNullOrEmpty(searchString))
        {
            var lowerSearchString = searchString.ToLower();
    
            Query.Where(type =>
                            type.Name.ToLower().Contains(lowerSearchString) ||
                            type.Description.ToLower().Contains(lowerSearchString) ||
                            type.Values.Any(v =>
                                                v.Value.ToLower().Contains(lowerSearchString) ||
                                                v.Description.ToLower().Contains(lowerSearchString)
                                           )
                       );
        }
        
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