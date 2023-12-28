using CommunityToolkit.Mvvm.ComponentModel;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Services;

namespace MyShop.ViewModels;

public partial class LoginControlViewModel : ObservableRecipient
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;
    private readonly IStoreLoginCredentialsService _storeLoginCredentialsService;

    [ObservableProperty]
    public string accessToken = string.Empty;

    [ObservableProperty]
    public bool isLoading = false;
    [ObservableProperty]
    public string errorMessage = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsRemembered
    {
        get; set;
    } = false;


    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);
    public bool IsNotAuthenticated => !IsAuthenticated;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsNotLoading => !IsLoading;

    public LoginControlViewModel(IAuthenticationService authenticationService, INavigationService navigationService, IStoreLoginCredentialsService storeLoginCredentialsService)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
        _storeLoginCredentialsService = storeLoginCredentialsService;
    }

    public async Task<(string Email, string Password)> GetStoredCredentialsAsync()
    {
        var isRemembered = await _storeLoginCredentialsService.TryGetRememberCredentialsAsync();
        IsRemembered = isRemembered;

        if (isRemembered)
        {
            var (email, password) = await _storeLoginCredentialsService.TryGetCredentialsAsync();
            Email = email;
            Password = password;
        }
        else
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        return (Email, Password);
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

            if (IsRemembered)
            {
                await _storeLoginCredentialsService.SaveCredentialsAsync(Email, Password);
            }
            else
            {
                await _storeLoginCredentialsService.ClearCredentailAsync();
            }

            await _storeLoginCredentialsService.SaveRememberCredentialsAsync(IsRemembered);
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
