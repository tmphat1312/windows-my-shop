using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void TimeFrame_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var radioButton = sender as RadioButton;
        if (radioButton != null)
        {
            ViewModel.SelectedType = radioButton.Content.ToString() switch
            {

                "Day" => "day",
                "Week" => "week",
                "Month" => "month",
                "Year" => "year",
                _ => "day"
            };
        }
    }

    private void Start_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
    {
        PickDateError.Text = "";

        var today = DateTimeOffset.Now;
        var sevenDaysAgo = today.AddDays(-7);

        var selectedDate = args.NewDate;

        if (selectedDate > today)
        {
            sender.Date = sevenDaysAgo;
            PickDateError.Text = "The start day is not greater than today";
        }
    }

    private void End_SelectedDateChanged(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
    {
        PickDateError.Text = "";

        var today = DateTimeOffset.Now;


        var selectedDate = args.NewDate;

        if (selectedDate > today)
        {
            sender.Date = today;
            PickDateError.Text = "The last date is not greater than today";
        }
    }
}
