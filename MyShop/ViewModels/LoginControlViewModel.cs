using CommunityToolkit.Mvvm.ComponentModel;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Services;

namespace MyShop.ViewModels;

public partial class LoginControlViewModel : ObservableRecipient
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public string accessToken = string.Empty;

    [ObservableProperty]
    public bool isLoading = false;
    [ObservableProperty]
    public string errorMessage = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;


    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);
    public bool IsNotAuthenticated => !IsAuthenticated;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsNotLoading => !IsLoading;

    public LoginControlViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
    }

    public async void LoginAsync()
    {
        IsLoading = true;
        NotifyChanges();

        var (message, ErrorCode) = await Task.Run(async () => await _authenticationService.LoginAsync(Email, Password));

        IsLoading = false;

        if (ErrorCode == 0)
        {
            AccessToken = _authenticationService.GetAccessToken();
            NotifyChanges();
            _navigationService.Refresh();
        }
        else
        {
            ErrorMessage = message;
            NotifyChanges();
        }
    }

    private void NotifyChanges()
    {
        OnPropertyChanged(nameof(IsAuthenticated));
        OnPropertyChanged(nameof(IsNotAuthenticated));
        OnPropertyChanged(nameof(HasError));
        OnPropertyChanged(nameof(IsNotLoading));
    }
}
