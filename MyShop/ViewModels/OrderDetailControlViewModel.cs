using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using Windows.Media.Capture;

namespace MyShop.ViewModels;
public partial class OrderDetailControlViewModel : ObservableObject
{
    private readonly IOrderDataService _orderDataservice;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public string newStatus = string.Empty;

    [ObservableProperty]
    public Order item = new();

    [ObservableProperty]
    public bool isEditSession = false;

    [ObservableProperty]
    public string editErrorMessage = string.Empty;

    [ObservableProperty]
    public bool isEditLoading = false;

    public bool IsEditButtonVisible => !IsEditSession;
    public bool HasEditError => !string.IsNullOrEmpty(EditErrorMessage);


    public RelayCommand CancelButtonCommand
    {
        get; set;
    }

    public RelayCommand EditCategoryButtonCommand
    {
        get; set;
    }

    public RelayCommand SetEditItemSessionButtonCommand
    {
        get; set;
    }


    public OrderDetailControlViewModel(IOrderDataService orderDataService, INavigationService navigationService)
    {
        _orderDataservice = orderDataService;
        _navigationService = navigationService;

        CancelButtonCommand = new RelayCommand(OnCancelEdit, () => !IsEditLoading);
        EditCategoryButtonCommand = new RelayCommand(OnUpdateOrder, () => !IsEditLoading);
        SetEditItemSessionButtonCommand = new RelayCommand(OnSetEditItemSession);

    }

    public void OnSetEditItemSession()
    {
        newStatus = Item.Status;
        IsEditSession = true;
        NotifyThisChanges();
    }

    public async void OnUpdateOrder()
    {
        IsEditLoading = true;

        if (newStatus == Item.Status)
        {
            EditErrorMessage = "Please change the status";
        }
        else
        {
            Item.Status = newStatus;
            var (returnedOrder, message, ERROR_CODE) = await _orderDataservice.UpdateOrderAsync(Item);
            if (ERROR_CODE == 0)
            {
                Item = returnedOrder;
                OnCancelEdit();
                _navigationService.Refresh();
            }
            else
            {
                EditErrorMessage = message;
            }
        }

        IsEditLoading = false;
        NotifyThisChanges();
    }

    public async void OnDeleteOrder()
    {
       
        var (_, ERROR_CODE) = await _orderDataservice.DeleteOrderAsync(Item);

        if (ERROR_CODE == 0)
        {
            _navigationService.Refresh();
        }

    }

    public void OnCancelEdit()
    {
        newStatus = Item.Status;
        IsEditSession = false;
        NotifyThisChanges();
    }

    public void NotifyThisChanges()
    {
        CancelButtonCommand.NotifyCanExecuteChanged();
        EditCategoryButtonCommand.NotifyCanExecuteChanged();
        SetEditItemSessionButtonCommand.NotifyCanExecuteChanged();
       
        OnPropertyChanged(nameof(IsEditButtonVisible));
        OnPropertyChanged(nameof(HasEditError));
        OnPropertyChanged(nameof(newStatus));
    }
}
