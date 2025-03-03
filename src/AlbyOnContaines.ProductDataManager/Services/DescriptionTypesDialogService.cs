using AlbyOnContainers.ProductDataManager.Domain;
using AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;
using AlbyOnContaines.ProductDataManager.Components.Pages.DescriptionsPage;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public class DescriptionTypesDialogService(DescriptionRepository repository, IDialogService dialog) : DialogServiceBase(dialog)
{
    public Task<DescriptionType?> OpenDialog(string title)
    {
        var type = new DescriptionType();

        return OpenDialog(title, type);
    }
    
    public async Task<DescriptionType?> OpenDialog(string title, DescriptionType type)
    {
        var parameters = new DialogParameters<DescriptionDialog>
        {
            { dialog => dialog.Type, type },
        };

        var dialog = await Dialog.ShowAsync<DescriptionDialog>(title, parameters, Options);
        var result = await dialog.Result;

        if (result?.Canceled ?? false) return null;

        if (type.Id == Guid.Empty)
            await repository.AddAsync(type);
        else
            await repository.UpdateAsync(type);

        return type;
    }

    public async Task OpenDeleteDialogAsync(DescriptionType type, string button, string content, string? title = null)
    {
        var result = await OpenDeleteDialogAsync(button, content, title);
        
        if (result?.Canceled ?? false) return;

        await repository.DeleteAsync(type);
    }
}