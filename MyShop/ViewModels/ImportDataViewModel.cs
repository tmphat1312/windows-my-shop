using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Helpers;

namespace MyShop.ViewModels;

public partial class ImportDataViewModel : ObservableRecipient
{
    [ObservableProperty]
    public bool isLoading = false;

    [ObservableProperty]
    public string errorMessage = string.Empty;

    [ObservableProperty]
    public string successMessage = string.Empty;

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    public bool HasSuccess => !string.IsNullOrEmpty(SuccessMessage);

    private readonly ICategoryDataService _categoryDataService;

    public ImportDataViewModel(ICategoryDataService categoryDataService)
    {
        _categoryDataService = categoryDataService;
        ImportCategoryButtonCommand = new RelayCommand(OnImportCategoryAsync, () => !IsLoading);
    }

    public RelayCommand ImportCategoryButtonCommand
    {
        get; set;
    }

    private async void OnImportCategoryAsync()
    {
        var categories = await ExcelDataModelReader.ReadCategoryFileAsync();

        IsLoading = true;
        NotifyChanges();


        var (message, ERROR_CODE) = await _categoryDataService.ImportDataAsync(categories);

        if (ERROR_CODE == 0)
        {
            SuccessMessage = message;
        }
        else
        {
            ErrorMessage = message;
        }

        IsLoading = false;
        NotifyChanges();
    }

    private void NotifyChanges()
    {
        ImportCategoryButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(HasError));
        OnPropertyChanged(nameof(HasSuccess));
    }
}
