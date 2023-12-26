using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using MyShop.Core.Models;

namespace MyShop.Views;

public sealed partial class OrdersDetailControl : UserControl
{
    public Order? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Order;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Order), typeof(OrdersDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public OrdersDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is OrdersDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
