﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using MyShop.Contracts.Services;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class BooksDetailPage : Page
{
    public BooksDetailViewModel ViewModel
    {
        get;
    }

    public BooksDetailPage()
    {
        ViewModel = App.GetService<BooksDetailViewModel>();
        InitializeComponent();
        Loaded += UpdateVisualState;
        SizeChanged += UpdateVisualState;
    }

    // Responsive 

    private void UpdateVisualState(object sender, RoutedEventArgs e)
    {
        var windowWidth = App.MainWindow.Width;

        if (windowWidth < 960)
        {
            DetailPanel.Orientation = Orientation.Vertical;
        }
        else
        {
            DetailPanel.Orientation = Orientation.Horizontal;
        }
    }


    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
