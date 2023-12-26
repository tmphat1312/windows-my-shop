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
    private Book newBook = new()
    {
        Quantity = 1,
        RatingsAverage = 0,
        PurchasePrice = 1000,
        SellingPrice = 1000,
        PublishedYear = DateTime.Now.Year,
        CategoryId = "6585555f703fca1356f60b91",
    };

    [ObservableProperty]
    private bool isLoading = false;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string successMessage = string.Empty;

    [ObservableProperty]
    private string selectedImageName = string.Empty;

    public bool IsImageSelected => !string.IsNullOrEmpty(SelectedImageName);
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool HasSuccess => !string.IsNullOrEmpty(SuccessMessage);


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

    public RelayCommand ResetButtonCommand
    {
        get; set;
    }

    public AddBookViewModel(IBookDataService bookDataService)
    {
        _bookDataService = bookDataService;

        SelectImageButtonCommand = new RelayCommand(SelectImage, () => !IsImageSelected);
        RemoveImageButtonCommand = new RelayCommand(RemoveImage, () => IsImageSelected);
        AddBookButtonCommand = new RelayCommand(AddBook, () => !IsLoading);
        ResetButtonCommand = new RelayCommand(Reset, () => !IsLoading);
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
        IsLoading = true;
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;
        NotfifyChanges();

        var (_, message, ERROR_CODE) = await _bookDataService.CreateBookAsync(NewBook);

        if (ERROR_CODE == 0)
        {
            SuccessMessage = message;
            Reset();
        }
        else
        {
            ErrorMessage = message;
        }

        IsLoading = false;
        NotfifyChanges();
    }

    public void Reset()
    {
        NewBook = new()
        {
            Quantity = 1,
            RatingsAverage = 0,
            PurchasePrice = 1000,
            SellingPrice = 1000,
            PublishedYear = DateTime.Now.Year,
            CategoryId = "6585555f703fca1356f60b91",
        };
        SelectedImageName = string.Empty;
        NotfifyChanges();
    }

    public void NotfifyChanges()
    {
        SelectImageButtonCommand.NotifyCanExecuteChanged();
        RemoveImageButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(IsImageSelected));
        OnPropertyChanged(nameof(HasError));
        OnPropertyChanged(nameof(HasSuccess));
    }
}
