using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using MyShop.Core.Models;

namespace MyShop.Views;

public sealed partial class CategoryDetailControl : UserControl
{
    public Category? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Category;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Category), typeof(CategoryDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public CategoryDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CategoryDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
