using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyShop.Services;

public partial class ResourceLoadingViewModel : ObservableRecipient
{
    [ObservableProperty]
    public bool isLoading;

    [ObservableProperty]
    public string? errorMessage;

    [ObservableProperty]
    public string? infoMessage;

    [ObservableProperty]
    public bool hasError;

    [ObservableProperty]
    public bool isReady;

    [ObservableProperty]
    public int currentPage;

    [ObservableProperty]
    public int totalPages;

    [ObservableProperty]
    public int totalItems;

    [ObservableProperty]
    public int itemsPerPage;

    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;

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
        CurrentPage = 1;
        TotalPages = 1;
        ItemsPerPage = 10;
        TotalItems = 0;

        GoToNextPageCommand = new RelayCommand(GoToNextPage, () => HasNextPage);
        GoToPreviousPageCommand = new RelayCommand(GoToPreviousPage, () => HasPreviousPage);
    }

    private void UpdateCommands()
    {
        GoToPreviousPageCommand.NotifyCanExecuteChanged();
        GoToNextPageCommand.NotifyCanExecuteChanged();
    }

    public virtual void GoToNextPage()
    {
        if (HasNextPage)
        {
            CurrentPage++;
            UpdateCommands();
        }
    }

    public virtual void GoToPreviousPage()
    {
        if (HasPreviousPage)
        {
            CurrentPage--;
            UpdateCommands();
        }
    }
}
