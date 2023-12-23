﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.ViewModels;

public partial class UsersViewModel : ObservableRecipient, INavigationAware
{
    private readonly IUserDataService _userDataService;

    [ObservableProperty]
    private User? selected;

    public ObservableCollection<User> UserItems { get; private set; } = new ObservableCollection<User>();

    public ObservableCollection<User> UserPage { get; private set; } = new ObservableCollection<User>();

    private const int ItemsPerPage = 4;

    [ObservableProperty]
    private int currentPage = 1;


    private int _totalPages;

    public RelayCommand GoToFirstPageCommand
    {
        get;
    }
    public RelayCommand GoToPreviousPageCommand
    {
        get;
    }
    public RelayCommand GoToNextPageCommand
    {
        get;
    }
    public RelayCommand GoToLastPageCommand
    {
        get;
    }

    public UsersViewModel(IUserDataService userDataService)
    {
        _userDataService = userDataService;

        currentPage = 1;

        GoToFirstPageCommand = new RelayCommand(GoToFirstPage, () => CurrentPage > 1);
        GoToPreviousPageCommand = new RelayCommand(GoToPreviousPage, () => CurrentPage > 1);
        GoToNextPageCommand = new RelayCommand(GoToNextPage, () => CurrentPage < _totalPages);
        GoToLastPageCommand = new RelayCommand(GoToLastPage, () => CurrentPage < _totalPages);
    }

    private void UpdateCommands()
    {
        GoToFirstPageCommand.RaiseCanExecuteChanged();
        GoToPreviousPageCommand.RaiseCanExecuteChanged();
        GoToNextPageCommand.RaiseCanExecuteChanged();
        GoToLastPageCommand.RaiseCanExecuteChanged();
    }

    private void GoToLastPage()
    {
        CurrentPage = _totalPages;
        UpdateUserPageForCurrentPageAsync();

    }
    private void GoToNextPage()
    {

        CurrentPage++;
        UpdateUserPageForCurrentPageAsync();

    }

    private void GoToFirstPage()
    {
        
        CurrentPage = 1;

        UpdateUserPageForCurrentPageAsync();

    }

    private void GoToPreviousPage()
    {

        CurrentPage--;

        UpdateUserPageForCurrentPageAsync();


    }

    public void UpdateUserPageForCurrentPageAsync()
    {
        var startIndex = (CurrentPage - 1) * ItemsPerPage; 

        var pageItems = UserItems.Skip(startIndex).Take(ItemsPerPage).ToList(); 

        UserPage.Clear(); 
        foreach (var item in pageItems)
        {
            UserPage.Add(item); 
        }

        EnsureItemSelected();
        UpdateCommands();

    }



    public async void OnNavigatedTo(object parameter)
    {
        UserItems.Clear();

        // TODO: Replace with real data.
        var data = await _userDataService.GetListUserDetailsDataAsync();

        foreach (var item in data)
        {
            UserItems.Add(item);

        }

        _totalPages = UserItems.Count / ItemsPerPage;
        if (UserItems.Count % ItemsPerPage != 0)
        {
            _totalPages++; // Thêm một trang nếu có phần tử dư
        }


        UpdateUserPageForCurrentPageAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        Selected = UserPage.Any() ? UserPage.First() : null;

    }
}
