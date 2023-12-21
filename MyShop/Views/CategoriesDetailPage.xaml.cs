using CommunityToolkit.WinUI.UI.Animations;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using MyShop.Contracts.Services;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class CategoriesDetailPage : Page
{
    public CategoriesDetailViewModel ViewModel
    {
        get;
    }

    public CategoriesDetailPage()
    {
        ViewModel = App.GetService<CategoriesDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
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
