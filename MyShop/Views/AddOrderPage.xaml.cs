﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Core.Http;
using MyShop.Core.Models;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class AddOrderPage : Page
{
    public AddOrderViewModel ViewModel
    {
        get;
    }

    public AddOrderPage()
    {
        ViewModel = App.GetService<AddOrderViewModel>();
        InitializeComponent();
        Loaded += UpdateVisualState;
        SizeChanged += UpdateVisualState;
    }

    private void UpdateVisualState(object sender, RoutedEventArgs e)
    {
        var windowWidth = App.MainWindow.Width;

        // VisualStateManager sucks

        if (windowWidth < 780)
        {
            FiltersAndSearchPanel.Visibility = Visibility.Collapsed;
            SmallFiltersAndSearchPanel.Visibility = Visibility.Visible;
        }
        else
        {
            FiltersAndSearchPanel.Visibility = Visibility.Visible;
            SmallFiltersAndSearchPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (sender as ComboBox)?.SelectedItem as HttpSortObject;

        if (selectedItem is not null)
        {
            ViewModel.SelectSortOption(selectedItem);
        }
    }


    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var text = (sender as TextBox)?.Text;

        if (text is not null)
        {
            ViewModel.Search(text);
        }
    }

    private void SearchTextBox_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {

            var text = (sender as TextBox)?.Text;

            if (text is not null)
            {
                ViewModel.Search(text);
                ViewModel.LoadData();
            }
        }
    }

    private void MinPriceTextBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        if (sender.Value > MaxPriceTextBox?.Value)
        {
            sender.Value = MaxPriceTextBox.Value;
            ViewModel.SetMinPrice((int)sender.Value);
        }
        else
        {
            ViewModel.SetMinPrice((int)sender.Value);
        }
    }

    private void MaxPriceTextBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        if (sender.Value < MinPriceTextBox?.Value)
        {
            sender.Value = MinPriceTextBox.Value;
            ViewModel.SetMaxPrice((int)sender.Value);
        }
        else
        {
            ViewModel.SetMaxPrice((int)sender.Value);
        }
    }

    private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (sender as ComboBox)?.SelectedItem as Category;

        if (selectedItem is not null)
        {
            ViewModel.SelectCategory(selectedItem);
        }
    }
}
