using Microsoft.UI.Xaml;

using MyShop.Contracts.Services;
using MyShop.ViewModels;

namespace MyShop.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;
    private readonly IStoreLastOpenPageService _storeLastOpenPageService;

    public DefaultActivationHandler(INavigationService navigationService, IStoreLastOpenPageService storeLastOpenPageService)
    {
        _navigationService = navigationService;
        _storeLastOpenPageService = storeLastOpenPageService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        var openLastPage = await _storeLastOpenPageService.GetOpenLastPageAsync();
        var lastOpenPage = await _storeLastOpenPageService.GetLastOpenPageAsync();

        if (openLastPage && !string.IsNullOrEmpty(lastOpenPage))
        {
            _navigationService.NavigateTo(lastOpenPage, args.Arguments);
            //_navigationService.Refresh();
        }
        else
        {
            _navigationService.NavigateTo(typeof(MainViewModel).FullName!, args.Arguments);
        }

        await Task.CompletedTask;
    }
}
