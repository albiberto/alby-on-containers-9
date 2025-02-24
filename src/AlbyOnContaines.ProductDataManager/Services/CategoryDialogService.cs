using AlbyOnContainers.ProductDataManager.Commands;
using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class CategoryDialogService(CategoryCommands commands, IDialogService dialog) : DialogServiceBase(dialog)
{
    public async Task OpenCreateCategoryDialog(string title, Guid? parentId = null)
    {
        var category = new Category
        {
            ParentId = parentId
        };
        
        var parameters = new DialogParameters<CategoryDialog> { { x => x.Category, category } };
        
        var dialog = await Dialog.ShowAsync<CategoryDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await commands.CreateRootCategoryAsync(category);
    }
    
    public async Task OpenEditCategoryDialog(string title, Category category)
    {
        var parameters = new DialogParameters<CategoryDialog> { { x => x.Category, category } };
        
        var dialog = await Dialog.ShowAsync<CategoryDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await commands.UpdateRootCategoryAsync();
    }

    public async Task OpenDeleteDialogAsync(Category category, string button, string content, string? title = null)
    {
        var result = await OpenDeleteDialogAsync(button, content, title);
        
        if (result?.Canceled ?? false) return;

        await commands.DeleteCategoryAsync(category);
    }
}