using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Services;

namespace MyShop.ViewModels;

public partial class OrdersViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly IOrderDataService _orderDataService;

    [ObservableProperty]
    private Order? selected;

    [ObservableProperty]
    public bool isContentReady = false;

    public ObservableCollection<Order> Source { get; private set; } = new ObservableCollection<Order>();


    public RelayCommand AddOrderCommand
    {
        get;
    }

    public RelayCommand DeleteOrderCommand
    {
        get;
    }

    public RelayCommand EditOrderCommand
    {
        get;
    }
    public OrdersViewModel(IOrderDataService OrderDataService, IStorePageSettingsService storePageSettingsService) : base(storePageSettingsService)
    {
        _orderDataService = OrderDataService;

        currentPage = 1;


        AddOrderCommand = new RelayCommand(AddOrder);
        DeleteOrderCommand = new RelayCommand(DeleteOrder, () => Selected != null);
        EditOrderCommand = new RelayCommand(EditOrder, () => Selected != null);
    }

    private void UpdateCommands()
    {

        DeleteOrderCommand.NotifyCanExecuteChanged();
        EditOrderCommand.NotifyCanExecuteChanged();
    }

    private void AddOrder()
    {

    }

    private void DeleteOrder()
    {

    }

    private void EditOrder()
    {

    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        IsLoading = true;
        IsContentReady = false;

        await Task.Run(async () => await _orderDataService.LoadDataAsync());

        var (data, totalItems, message, ERROR_CODE) = _orderDataService.GetData();

        if (data is not null)
        {
            Source.Clear();

            foreach (var item in data)
            {
                Source.Add(item);
            }

            TotalItems = totalItems;

            if (TotalItems == 0)
            {
                InfoMessage = "No orders found";
            }
        }
        else
        {
            if (ERROR_CODE != 0)
            {
                ErrorMessage = message;
            }
        }

        IsLoading = false;
        IsContentReady = true;

        EnsureItemSelected();
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        Selected = Source.Any() ? Source.First() : null;

    }
}
