using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class CategoryDialogService(CategoryRepository repository, IDialogService dialog) : DialogServiceBase(dialog)
{
    public async Task OpenCreateCategoryDialog(string title, List<Category> categories, Guid? parentId = null)
    {
        var category = new Category
        {
            ParentId = parentId
        };
        
        var parameters = new DialogParameters<CategoryDialog>
        {
            { dialog => dialog.Category, category },
            { dialog => dialog.Categories, categories }
        };
        
        var dialog = await Dialog.ShowAsync<CategoryDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await repository.AddAsync(category);
    }
    
    public async Task OpenEditCategoryDialog(string title, List<Category> categories, Category category)
    {
        var parameters = new DialogParameters<CategoryDialog>
        {
            { dialog => dialog.Category, category },
            { dialog => dialog.Categories, categories }
        };
        
        var dialog = await Dialog.ShowAsync<CategoryDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await repository.UpdateAsync(category);
    }

    public async Task OpenDeleteDialogAsync(Category category, string button, string content, string? title = null)
    {
        var result = await OpenDeleteDialogAsync(button, content, title);
        
        if (result?.Canceled ?? false) return;

        await repository.DeleteAsync(category);
    }
}