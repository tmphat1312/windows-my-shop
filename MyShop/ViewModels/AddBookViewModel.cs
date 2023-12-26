using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace MyShop.ViewModels;

public partial class AddBookViewModel : ObservableRecipient
{
    private readonly IBookDataService _bookDataService;

    [ObservableProperty]
    private string successMessage = string.Empty;

    [ObservableProperty]
    private Book newBook = new();

    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string selectedImageName = string.Empty;

    public bool IsImageSelected => !string.IsNullOrEmpty(SelectedImageName);
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);


    public RelayCommand SelectImageButtonCommand
    {
        get; set;
    }

    public RelayCommand RemoveImageButtonCommand
    {
        get; set;
    }

    public RelayCommand AddBookButtonCommand
    {
        get; set;
    }

    public AddBookViewModel(IBookDataService bookDataService)
    {
        _bookDataService = bookDataService;

        SelectImageButtonCommand = new RelayCommand(SelectImage, () => !IsImageSelected);
        RemoveImageButtonCommand = new RelayCommand(RemoveImage, () => IsImageSelected);
        AddBookButtonCommand = new RelayCommand(AddBook);
    }

    public async void SelectImage()
    {
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.PicturesLibrary
        };
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");

        // Initialize the picker with the window handle
        var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(picker, hwnd);

        var file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            SelectedImageName = file.Name;
            using var stream = await file.OpenStreamForReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            NewBook.ImageBytes = memoryStream.ToArray();
        }

        NotfifyChanges();
    }

    public void RemoveImage()
    {
        NewBook.ImageBytes = null;
        SelectedImageName = string.Empty;

        NotfifyChanges();
    }

    public async void AddBook()
    {
        //Message = "Processing!!!";
        //var respone = await _userDataService.CreateUserAsync(newUser);
        //Message = respone.Message;

        //if (respone.ErrorCode == 201)
        //{
        //    _navigationService.NavigateTo("MyShop.ViewModels.UsersViewModel");
        //}
    }

    public void NotfifyChanges()
    {
        SelectImageButtonCommand.NotifyCanExecuteChanged();
        RemoveImageButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(IsImageSelected));
        OnPropertyChanged(nameof(HasError));
    }
}
