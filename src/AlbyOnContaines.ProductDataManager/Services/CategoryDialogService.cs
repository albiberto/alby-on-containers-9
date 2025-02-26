using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContaines.ProductDataManager.Components.Pages.CategoriesPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class CategoryDialogService(CategoryRepository repository, IDialogService dialog) : DialogServiceBase(dialog)
{
    public Task<Category?> OpenCategoryDialog(string title, List<Category> categories, Guid? parentId = null)
    {
        var category = new Category
        {
            ParentId = parentId
        };

        return OpenCategoryDialog(title, categories, category);
    }
    
    public async Task<Category?> OpenCategoryDialog(string title, List<Category> categories, Category category)
    {
        var parameters = new DialogParameters<CategoryDialog>
        {
            { dialog => dialog.Category, category },
            { dialog => dialog.Categories, categories }
        };

        var dialog = await Dialog.ShowAsync<CategoryDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return null;

        if (category.Id == Guid.Empty)
            await repository.AddAsync(category);
        else
            await repository.UpdateAsync(category);

        return category;
    }

    public async Task OpenDeleteDialogAsync(Category category, string button, string content, string? title = null)
    {
        var result = await OpenDeleteDialogAsync(button, content, title);
        
        if (result?.Canceled ?? false) return;

        await repository.DeleteAsync(category);
    }
}