using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Helpers;
using MyShop.Views;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace MyShop.ViewModels;

public partial class AddUserViewModel : ObservableRecipient
{
    private readonly IUserDataService _userDataService;

    private INavigationService _navigationService;

    [ObservableProperty]
    private string message;

    [ObservableProperty]
    private User newUser;

    [ObservableProperty]
    private string selectedImageName;

    public RelayCommand SelectImageButtonCommand
    {
        get;set;
    }

    public RelayCommand RemoveImageButtonCommand
    {
        get; set;
    }

    public RelayCommand CreateUserButtonCommand
    {
        get; set;
    }

    public AddUserViewModel(IUserDataService userDataService,INavigationService navigationService)
    {
        this._userDataService = userDataService;
        this._navigationService = navigationService;

        newUser = new User();
        SelectImageButtonCommand = new RelayCommand(SelectImageButton);
        CreateUserButtonCommand = new RelayCommand(CreateUser);
        RemoveImageButtonCommand = new RelayCommand(() => { newUser.ImageBytes = null; SelectedImageName = null; });
                                     
    }

    public async void SelectImageButton()
    {
        var picker = new FileOpenPicker();
        picker.ViewMode = PickerViewMode.Thumbnail;
        picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");

        // Initialize the picker with the window handle
        IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(picker, hwnd);

        var file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            SelectedImageName = file.Name;
            using (var stream = await file.OpenStreamForReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    newUser.ImageBytes = memoryStream.ToArray();
                }
            }
        }
    }

    public async void CreateUser()
    {
        Message = "Processing!!!";
        var respone = await _userDataService.CreateUserAsync(newUser);
        Message = respone.Message;

        if(respone.ErrorCode==201)
        {
            _navigationService.NavigateTo("MyShop.ViewModels.UsersViewModel");
        }
    }


}
