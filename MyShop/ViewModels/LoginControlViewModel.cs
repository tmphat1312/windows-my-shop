using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Services;

namespace MyShop.ViewModels;

public partial class LoginControlViewModel : ObservableRecipient
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;
    private readonly IStoreLoginCredentialsService _storeLoginCredentialsService;
    private readonly IStoreServerOriginService _storeServerOriginService;

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
    [ObservableProperty]
    public string serverHost = "http://localhost";
    [ObservableProperty]
    public int serverPort = 8080;


    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);
    public bool IsNotAuthenticated => !IsAuthenticated;
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool IsNotLoading => !IsLoading;
    public bool IsSavingSettings
    {
        get; set;
    } = false;

    public RelayCommand SaveSettingsCommand
    {
        get; set;
    }

    public LoginControlViewModel(IAuthenticationService authenticationService, INavigationService navigationService, IStoreLoginCredentialsService storeLoginCredentialsService, IStoreServerOriginService storeServerOriginService)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
        _storeLoginCredentialsService = storeLoginCredentialsService;
        _storeServerOriginService = storeServerOriginService;

        SaveSettingsCommand = new RelayCommand(SaveServerOriginSettingsAsync, () => !IsSavingSettings);
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

    public async Task<(string Host, int Port)> GetStoredServerOriginAsync()
    {
        var (host, port) = await _storeServerOriginService.TryGetServerOriginAsync();
        ServerHost = host;
        ServerPort = port;

        return (ServerHost, ServerPort);
    }

    public async void SaveServerOriginSettingsAsync()
    {
        IsSavingSettings = true;
        NotifyChanges();
        await _storeServerOriginService.SaveServerOriginAsync(ServerHost, ServerPort);
        IsSavingSettings = false;
        NotifyChanges();
    }

    private void NotifyChanges()
    {
        SaveSettingsCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(IsAuthenticated));
        OnPropertyChanged(nameof(IsNotAuthenticated));
        OnPropertyChanged(nameof(HasError));
        OnPropertyChanged(nameof(IsNotLoading));
    }
}
