﻿using System.Reflection;
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

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IStorePageSettingsService storePageSettingsService)
    {
        _themeSelectorService = themeSelectorService;
        _storePageSettingsService = storePageSettingsService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();

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
    }

    public async void SaveItemsPerPage()
    {
        await _storePageSettingsService.SaveItemsPerPageAsync(ItemsPerPage);
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
