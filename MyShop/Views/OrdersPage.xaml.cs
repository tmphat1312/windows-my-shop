using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class OrdersPage : Page
{
    public OrdersViewModel ViewModel
    {
        get;
    }

    public OrdersPage()
    {
        ViewModel = App.GetService<OrdersViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }

    private void FromDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        var date = FromDate.Date;
        var convertedDate = date.HasValue ? date.Value.DateTime : DateTime.MinValue;
        ViewModel.SetFromDate(convertedDate);
    }

    private void ToDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        var date = ToDate.Date;
        var convertedDate = date.HasValue ? date.Value.DateTime : DateTime.MinValue;
        ViewModel.SetToDate(convertedDate);
    }
}
