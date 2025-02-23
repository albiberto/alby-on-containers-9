using AlbyOnContainers.ProductDataManager.Commands;
using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class CategoryDialogService(CategoryCommands commands, IDialogService service)
{
    public async Task OpenCreateCategoryDialog(string title, Guid? parentId = null)
    {
        var category = new Category
        {
            ParentId = parentId
        };
        
        var parameters = new DialogParameters<CategoryDialog> { { x => x.Category, category } };
        var options = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true };
        
        var dialog = await service.ShowAsync<CategoryDialog>(title, parameters, options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await commands.CreateRootCategoryAsync(category);
    }
    
    public async Task OpenEditCategoryDialog(string title, Category category)
    {
        var parameters = new DialogParameters<CategoryDialog> { { x => x.Category, category } };
        var options = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true };
        
        var dialog = await service.ShowAsync<CategoryDialog>(title, parameters, options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await commands.UpdateRootCategoryAsync();
    }
}