namespace MyShop.Contracts.Services;
public interface IResourcePagingService
{
    public int CurrentPage
    {
        get; set;
    }
    public int TotalPages
    {
        get; set;
    }
    public int ItemsPerPage
    {
        get; set;
    }
    public int TotalItems
    {
        get; set;
    }

    public bool HasNextPage();
    public bool HasPreviousPage();
    public bool IsFirstPage();
    public bool IsLastPage();
    public void GoToNextPage();
    public void GoToPreviousPage();
}
