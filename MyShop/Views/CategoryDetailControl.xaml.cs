﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using MyShop.Core.Models;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class CategoryDetailControl : UserControl
{
    public CategoryDetailControlViewModel ViewModel
    {
        get;
    }

    public Category? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as Category;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }


    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(Category), typeof(CategoryDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public CategoryDetailControl()
    {
        InitializeComponent();
        ViewModel = App.GetService<CategoryDetailControlViewModel>();
        UpdateItem();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CategoryDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
            control.UpdateItem();
        }
    }

    private void UpdateItem()
    {
        ViewModel.Item = ListDetailsMenuItem ?? new();
        ViewModel.EditCategory = ListDetailsMenuItem ?? new();
    }

    private async void DeleteItemButton_Click(object sender, RoutedEventArgs e)
    {
        var deleteFileDialog = new ContentDialog
        {
            Title = "Delete this category permanently?",
            Content = "If you delete this category, you won't be able to recover it. Do you want to delete it?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            XamlRoot = ForegroundElement.XamlRoot
        };
        var result = await deleteFileDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            ViewModel.OnDeletCategory();
        }
    }
}
