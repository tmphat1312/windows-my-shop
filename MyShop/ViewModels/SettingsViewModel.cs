using System.Reflection;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using MyShop.Contracts.Services;
using MyShop.Helpers;

using Windows.ApplicationModel;

namespace MyShop.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IStorePageSettingsService _storePageSettingsService;
    private readonly IStoreLastOpenPageService _storeLastOpenPageService;

    [ObservableProperty]
    private ElementTheme _elementTheme;

    [ObservableProperty]
    private string _versionDescription;

    public List<int> ItemsPerPageOptions
    {
        get;
    } = new() { 5, 10, 15, 20 };

    [ObservableProperty]
    public int itemsPerPage = 10;

    [ObservableProperty]
    public bool openLastPage;

    public bool IsFirstLaunch
    {
        get;
        set;
    } = true;

    public ICommand SwitchThemeCommand
    {
        get;
    }


    public SettingsViewModel(IThemeSelectorService themeSelectorService, IStorePageSettingsService storePageSettingsService, IStoreLastOpenPageService storeLastOpenPageService)
    {
        _themeSelectorService = themeSelectorService;
        _storePageSettingsService = storePageSettingsService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        _storeLastOpenPageService = storeLastOpenPageService;

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        ItemsPerPage = await _storePageSettingsService.GetItemsPerPageAsync();
        OpenLastPage = await _storeLastOpenPageService.GetOpenLastPageAsync();
        await Task.CompletedTask;
    }

    public async void SaveItemsPerPage()
    {
        await _storePageSettingsService.SaveItemsPerPageAsync(ItemsPerPage);
        await Task.CompletedTask;
    }

    public async void SaveOpenLastPage()
    {
        await _storeLastOpenPageService.SaveOpenLastPageAsync(true);
        await Task.CompletedTask;
    }

    public async void SaveNotOpenLastPage()
    {
        await _storeLastOpenPageService.SaveOpenLastPageAsync(false);
        await Task.CompletedTask;
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
