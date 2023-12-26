using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Services;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace MyShop.ViewModels;

public partial class BooksDetailViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly IReviewDataService _reviewDataService;
    private readonly IBookDataService _bookDataService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    public bool isEditSession = false;

    public bool IsEditButtonVisible => !IsEditSession;

    [ObservableProperty]
    public string editErrorMessage = string.Empty;
    [ObservableProperty]
    public bool isEditLoading = false;


    [ObservableProperty]
    public string selectedImageName = string.Empty;

    public bool IsImageSelected => !string.IsNullOrEmpty(SelectedImageName);
    public bool HasEditError => !string.IsNullOrEmpty(EditErrorMessage);


    [ObservableProperty]
    public Book? item;

    [ObservableProperty]
    public Book? editBook;


    public ObservableCollection<Review> Source { get; } = new ObservableCollection<Review>();

    public RelayCommand SetEditItemSessionButtonCommand
    {
        get; set;
    }

    public RelayCommand SelectImageButtonCommand
    {
        get; set;
    }

    public RelayCommand RemoveImageButtonCommand
    {
        get; set;
    }

    public RelayCommand CancelButtonCommand
    {
        get; set;
    }

    public RelayCommand EditBookButtonCommand
    {
        get; set;
    }

    public BooksDetailViewModel(IReviewDataService reviewDataService, IBookDataService bookDataService, INavigationService navigationService)
    {
        _reviewDataService = reviewDataService;
        _bookDataService = bookDataService;
        _navigationService = navigationService;

        SetEditItemSessionButtonCommand = new RelayCommand(() =>
        {
            IsEditSession = true;
            NotifyThisChanges();
        });

        SelectImageButtonCommand = new RelayCommand(SelectImage, () => !IsImageSelected);
        RemoveImageButtonCommand = new RelayCommand(RemoveImage);
        CancelButtonCommand = new RelayCommand(CancelEdit, () => !IsEditLoading);
        EditBookButtonCommand = new RelayCommand(UpdateBook, () => !IsEditLoading);
    }

    public async void SelectImage()
    {
        if (EditBook is null)
        {
            return;
        }

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
            EditBook.ImageBytes = memoryStream.ToArray();
        }

        NotifyThisChanges();
    }

    public void RemoveImage()
    {
        EditBook.ImageBytes = null;
        SelectedImageName = string.Empty;

        NotifyThisChanges();
    }

    public void NotifyThisChanges()
    {
        SetEditItemSessionButtonCommand.NotifyCanExecuteChanged();
        CancelButtonCommand.NotifyCanExecuteChanged();
        RemoveImageButtonCommand.NotifyCanExecuteChanged();
        SelectImageButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(IsEditButtonVisible));
        OnPropertyChanged(nameof(IsImageSelected));
        OnPropertyChanged(nameof(HasEditError));
    }

    public async void LoadReviewsAsync()
    {
        IsLoading = true;
        InfoMessage = string.Empty;
        ErrorMessage = string.Empty;
        NotfifyChanges();

        await Task.Run(async () => await _reviewDataService.LoadDataAsync());

        var (data, totalItems, message, ERROR_CODE) = _reviewDataService.GetData();

        if (data is not null)
        {
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.Content))
                {
                    item.Content = "No review content";
                }
                Source.Add(item);
            }

            IsLoading = false;
            TotalItems = totalItems;

            if (TotalItems == 0)
            {
                InfoMessage = "This book hasn't had any reviews";
            }
        }
        else
        {
            if (ERROR_CODE != 0)
            {
                ErrorMessage = message;
            }
        }

        NotfifyChanges();
    }

    public async void DeleteBook()
    {
        var (_, ERROR_CODE) = await _bookDataService.DeleteBookAsync(Item);

        if (ERROR_CODE == 0)
        {
            _navigationService.GoBack();
        }
    }

    public async void UpdateBook()
    {
        IsEditLoading = true;

        var (returnedBook, message, ERROR_CODE) = await _bookDataService.UpdateBookAsync(EditBook);

        if (ERROR_CODE == 0)
        {
            Item = returnedBook;
            CancelEdit();
        }
        else
        {
            EditErrorMessage = message;
        }

        IsEditLoading = false;
        NotifyThisChanges();
    }

    public void CancelEdit()
    {
        EditBook = Item;
        IsEditSession = false;
        SelectedImageName = string.Empty;
        NotifyThisChanges();
    }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is Book book)
        {
            Item = book;
            EditBook = Item;
            _reviewDataService.BookId = Item?.Id ?? string.Empty;
            LoadReviewsAsync();
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
