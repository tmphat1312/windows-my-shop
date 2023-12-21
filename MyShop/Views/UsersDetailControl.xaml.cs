using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using MyShop.Core.Models;

namespace MyShop.Views;

public sealed partial class UsersDetailControl : UserControl
{
    public SampleOrder? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as SampleOrder;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SampleOrder), typeof(UsersDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public UsersDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UsersDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
