using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContaines.ProductDataManager.Services;
using AlbyOnContaines.ProductDataManager.Specifications;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;

public partial class Categories : ComponentBase
{
    [Inject] CategoryDialogService CategoryDialogService { get; set; } = null!;
    [Inject] CategoryRepository CategoryRepository { get; set; } = null!;
    [Inject] ISnackbar Snackbar { get; set; } = null!;

    MudTreeView<Category>? _treeView;

    List<Category> _categories = [];
    List<TreeItemData<Category>> _items = [];

    string _searchPhrase = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        var categories = await CategoryRepository.ListAsync(new CategoryWithParentSpecification());

        _categories = categories.OrderBy(category => category.Name).ToList();
        _items = GetAllHierarchy(_categories).ToList();
    }

    static IEnumerable<TreeItemData<Category>> GetAllHierarchy(List<Category> categories)
    {
        var lookup = categories.ToLookup(c => c.ParentId);

        foreach (var item in BuildTree(null)) yield return item;

        yield break;

        IEnumerable<TreeItemData<Category>> BuildTree(Guid? parentId)
        {
            foreach (var category in lookup[parentId])
            {
                var children = BuildTree(category.Id).ToList();
                var item = new TreeItemData<Category>
                {
                    Text = category.Name,
                    Value = category,
                    Expandable = children.Count != 0,
                    Children = children
                };
                yield return item;
            }
        }
    }

    async Task OnTextChanged(string searchPhrase)
    {
        _searchPhrase = searchPhrase;
        if (_treeView is not null) await _treeView.FilterAsync();
    }

    Task<bool> MatchesName(TreeItemData<Category> item)
    {
        var match = string.IsNullOrEmpty(item.Text) || item.Text.Contains(_searchPhrase, StringComparison.OrdinalIgnoreCase);
        return Task.FromResult(match);
    }

    async Task OpenCreateCategoryDialog(Guid? parentId = null)
    {
        var category = await CategoryDialogService.OpenCategoryDialog("Creare Category", _categories, parentId);

        if (category is null) return;

        var index = _categories.BinarySearch(category, Comparer<Category>.Create((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal)));
        if (index < 0) index = ~index;
        _categories.Insert(index, category);

        _items = GetAllHierarchy(_categories).ToList();
        
        StateHasChanged();
    }

    async Task OpenEditCategoryDialog(Category category)
    {
        await CategoryDialogService.OpenCategoryDialog("Edit Category", _categories, category);
        StateHasChanged();
    }

    async Task OpenDeleteCategoryDialog(Category category)
    {
        await CategoryDialogService.OpenDeleteDialogAsync(category, "Delete", $@"Do you really want to delete ""{category.Name}"" category? This process cannot be undone.", "Delete Category?");

        _categories.Remove(category);
        _items = GetAllHierarchy(_categories).ToList();

        StateHasChanged();
    }

    Task ExpandAll() => _treeView?.ExpandAllAsync() ?? Task.CompletedTask;
    Task CollapseAll() => _treeView?.CollapseAllAsync() ?? Task.CompletedTask;
}