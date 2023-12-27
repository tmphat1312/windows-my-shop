using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Helpers;

namespace MyShop.ViewModels;

public partial class ImportDataViewModel : ObservableRecipient
{
    [ObservableProperty]
    public bool isLoadingCategory = false;

    [ObservableProperty]
    public string errorMessageCategory = string.Empty;

    [ObservableProperty]
    public string successMessageCategory = string.Empty;

    public bool HasErrorCategory => !string.IsNullOrEmpty(ErrorMessageCategory);
    public bool HasSuccessCategory => !string.IsNullOrEmpty(SuccessMessageCategory);

    [ObservableProperty]
    public bool isLoadingBook = false;

    [ObservableProperty]
    public string errorMessageBook = string.Empty;

    [ObservableProperty]
    public string successMessageBook = string.Empty;

    public bool HasErrorBook => !string.IsNullOrEmpty(ErrorMessageBook);
    public bool HasSuccessBook => !string.IsNullOrEmpty(SuccessMessageBook);


    private readonly ICategoryDataService _categoryDataService;
    private readonly IBookDataService _bookDataService;

    public ImportDataViewModel(ICategoryDataService categoryDataService, IBookDataService bookDataService)
    {
        _categoryDataService = categoryDataService;
        _bookDataService = bookDataService;

        ImportCategoryButtonCommand = new RelayCommand(OnImportCategoryAsync, () => !IsLoadingCategory && !IsLoadingBook);
        ImportBookButtonCommand = new RelayCommand(OnImportBookAsync, () => !IsLoadingCategory && !IsLoadingBook);
    }

    public RelayCommand ImportCategoryButtonCommand
    {
        get; set;
    }

    public RelayCommand ImportBookButtonCommand
    {
        get; set;
    }

    private async void OnImportCategoryAsync()
    {
        var categories = await ExcelDataModelReader.ReadCategoryFileAsync();

        IsLoadingCategory = true;
        NotifyChanges();

        var (message, ERROR_CODE) = await _categoryDataService.ImportDataAsync(categories);

        if (ERROR_CODE == 0)
        {
            SuccessMessageCategory = message;
        }
        else
        {
            ErrorMessageCategory = message;
        }

        IsLoadingCategory = false;
        NotifyChanges();
    }

    private async void OnImportBookAsync()
    {
        try
        {
            var books = await ExcelDataModelReader.ReadBookFileAsync();

            IsLoadingBook = true;
            NotifyChanges();

            var (message, ERROR_CODE) = await _bookDataService.ImportDataAsync(books);

            if (ERROR_CODE == 0)
            {
                SuccessMessageBook = message;
            }
            else
            {
                ErrorMessageBook = message;
            }

            IsLoadingBook = false;
        }
        catch (Exception ex)
        {
            ErrorMessageBook = ex.Message;
        }
        finally
        {
            NotifyChanges();
        }
    }

    private void NotifyChanges()
    {
        ImportCategoryButtonCommand.NotifyCanExecuteChanged();
        ImportBookButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(HasErrorCategory));
        OnPropertyChanged(nameof(HasSuccessCategory));
        OnPropertyChanged(nameof(HasErrorBook));
        OnPropertyChanged(nameof(HasSuccessBook));
    }
}
