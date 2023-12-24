using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Helpers;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace MyShop.ViewModels;

public partial class AddUserViewModel : ObservableRecipient
{

    private StorageFile selectedImageFile;

    public RelayCommand SelectImageButtonCommand
    {
        get;set;
    }

    public RelayCommand CreateUserButtonCommand
    {
        get; set;
    }

    public AddUserViewModel()
    {
        SelectImageButtonCommand = new RelayCommand(SelectImageButton);
        CreateUserButtonCommand = new RelayCommand(CreateUser);
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

        selectedImageFile = await picker.PickSingleFileAsync();
    }

    public void CreateUser()
    {

    }


}
