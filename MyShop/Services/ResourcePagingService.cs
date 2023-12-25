using CommunityToolkit.Mvvm.ComponentModel;
using MyShop.Contracts.Services;

namespace MyShop.Services;

public partial class ResourcePagingService : ObservableRecipient, IResourcePagingService
{
    [ObservableProperty]
    public int currentPage = 1;
    [ObservableProperty]
    public int totalPages = 6;
    [ObservableProperty]
    public int itemsPerPage = 10;
    [ObservableProperty]
    public int totalItems = 0;

    public void GoToNextPage() => throw new NotImplementedException();
    public void GoToPreviousPage() => throw new NotImplementedException();
    public bool HasNextPage() => throw new NotImplementedException();
    public bool HasPreviousPage() => throw new NotImplementedException();
    public bool IsFirstPage() => throw new NotImplementedException();
    public bool IsLastPage() => throw new NotImplementedException();
}
