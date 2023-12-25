﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Core.Helpers;

namespace MyShop.Services;

public partial class ResourceLoadingViewModel : ObservableRecipient
{
    [ObservableProperty]
    public bool isLoading;

    [ObservableProperty]
    public string? errorMessage = string.Empty;

    [ObservableProperty]
    public string? infoMessage = string.Empty;

    [ObservableProperty]
    public int currentPage = 1;

    [ObservableProperty]
    public int totalItems = 0;

    [ObservableProperty]
    public int itemsPerPage = 10;

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool HasInfo => !string.IsNullOrEmpty(InfoMessage);
    public bool ContentIsReady => !IsLoading && !HasError && !HasInfo;
    public int From => (CurrentPage - 1) * ItemsPerPage + 1;
    public int To => Math.Min(CurrentPage * ItemsPerPage, TotalItems);
    public bool ShowPagination => TotalPages > 1;

    public Action FunctionOnCommand { get; set; } = () => { };

    public RelayCommand GoToNextPageCommand
    {
        get;
    }

    public RelayCommand GoToPreviousPageCommand
    {
        get;
    }

    public ResourceLoadingViewModel()
    {
        GoToNextPageCommand = new RelayCommand(GoToNextPage, () => HasNextPage);
        GoToPreviousPageCommand = new RelayCommand(GoToPreviousPage, () => HasPreviousPage);
        ItemsPerPage = 2; // TODO: get from settings
    }

    protected string BuildSearchParams()
    {
        var paramBuilder = new HttpSearchParamsBuilder();

        paramBuilder.Append("page", CurrentPage);
        paramBuilder.Append("limit", ItemsPerPage);

        return paramBuilder.GetQueryString();
    }

    public void NotfifyChanges()
    {
        GoToPreviousPageCommand.NotifyCanExecuteChanged();
        GoToNextPageCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(HasNextPage));
        OnPropertyChanged(nameof(HasPreviousPage));
        OnPropertyChanged(nameof(HasError));
        OnPropertyChanged(nameof(HasInfo));
        OnPropertyChanged(nameof(ContentIsReady));
        OnPropertyChanged(nameof(TotalPages));
        OnPropertyChanged(nameof(From));
        OnPropertyChanged(nameof(To));
        OnPropertyChanged(nameof(ShowPagination));
    }

    public virtual void GoToNextPage()
    {
        if (HasNextPage)
        {
            CurrentPage++;
            NotfifyChanges();
            FunctionOnCommand();
        }
    }

    public virtual void GoToPreviousPage()
    {
        if (HasPreviousPage)
        {
            CurrentPage--;
            NotfifyChanges();
            FunctionOnCommand();
        }
    }
}