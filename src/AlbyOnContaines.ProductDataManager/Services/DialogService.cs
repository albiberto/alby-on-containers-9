using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class DialogService(IDialogService service)
{
    public async Task OpenCategoryDialog(string title, Func<Category,Task> selector)
    {
        var category = new Category();
        
        var parameters = new DialogParameters<CategoryDialog> { { x => x.Category, category } };
        var options = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true };
        
        var dialog = await service.ShowAsync<CategoryDialog>(title, parameters, options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return;

        await selector(category);
    }
}