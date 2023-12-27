using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.ViewModels;
public partial class CategoryDetailControlViewModel : ObservableRecipient
{
    private readonly ICategoryDataService _categoryDataservice;

    [ObservableProperty]
    public Category editCategory = new();

    [ObservableProperty]
    public Category item = new();

    [ObservableProperty]
    public bool isEditSession = false;

    [ObservableProperty]
    public string editErrorMessage = string.Empty;

    [ObservableProperty]
    public bool isEditLoading = false;

    public bool IsEditButtonVisible => !IsEditSession;
    public bool HasEditError => !string.IsNullOrEmpty(EditErrorMessage);


    public RelayCommand CancelButtonCommand
    {
        get; set;
    }

    public RelayCommand EditCategoryButtonCommand
    {
        get; set;
    }

    public RelayCommand SetEditItemSessionButtonCommand
    {
        get; set;
    }

    public CategoryDetailControlViewModel(ICategoryDataService categoryDataService)
    {
        _categoryDataservice = categoryDataService;

        CancelButtonCommand = new RelayCommand(OnCancelEdit, () => !IsEditLoading);
        EditCategoryButtonCommand = new RelayCommand(OnUpdateCategory, () => !IsEditLoading);
        SetEditItemSessionButtonCommand = new RelayCommand(OnSetEditItemSession);
    }


    public void OnSetEditItemSession()
    {
        EditCategory = Item;
        IsEditSession = true;
        NotifyThisChanges();
    }

    public async void OnUpdateCategory()
    {
        IsEditLoading = true;

        var (returnedBook, message, ERROR_CODE) = await _categoryDataservice.UpdateCategoryAsync(EditCategory);

        if (ERROR_CODE == 0)
        {
            Item = returnedBook;
            OnCancelEdit();
        }
        else
        {
            EditErrorMessage = message;
        }

        IsEditLoading = false;
        NotifyThisChanges();
    }

    public void OnCancelEdit()
    {
        EditCategory = Item;
        IsEditSession = false;
        NotifyThisChanges();
    }

    private void NotifyThisChanges()
    {
        CancelButtonCommand.NotifyCanExecuteChanged();
        EditCategoryButtonCommand.NotifyCanExecuteChanged();
        SetEditItemSessionButtonCommand.NotifyCanExecuteChanged();

        OnPropertyChanged(nameof(IsEditButtonVisible));
        OnPropertyChanged(nameof(HasEditError));
    }
}
