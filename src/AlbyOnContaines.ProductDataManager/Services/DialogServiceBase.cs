using AlbyOnContaines.ProductDataManager.Components.Shared;
using MudBlazor;

namespace AlbyOnContaines.ProductDataManager.Services;

public abstract class DialogServiceBase(IDialogService dialog)
{
    protected readonly IDialogService Dialog = dialog;

    protected readonly DialogOptions Options = new() { MaxWidth = MaxWidth.Medium, FullWidth = true, BackdropClick = true, CloseButton = true};

    protected async Task<DialogResult?> OpenDeleteDialogAsync(string button, string content, string? title = null, Color color = Color.Error)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.Button, button },
            { x => x.Content, content },
            { x => x.Color, color }
        };
        
        var dialog = await Dialog.ShowAsync<ConfirmDialog>(title ?? button, parameters, Options);
        return await dialog.Result;
    }
}