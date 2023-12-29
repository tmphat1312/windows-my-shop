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

    private void StartDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        PickDateError.Text = "";

        var date = StartDate.Date;

        var today = DateTimeOffset.Now;
        var sevenDaysAgo = today.AddDays(-7);

        if (date > today)
        {
            sender.Date = sevenDaysAgo;
            PickDateError.Text = "The start day is not greater than today";
            return;
        }

        ViewModel.StartDate = date.Value.DateTime;
        ViewModel.IsDirty = true;
    }

    private void EndDate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        PickDateError.Text = "";

        var date = EndDate.Date;
        var today = DateTimeOffset.Now;

        if (date > today)
        {
            sender.Date = today;
            PickDateError.Text = "The last date is not greater than today";
            return;
        }

        ViewModel.LastDate = date.Value.DateTime;
        ViewModel.IsDirty = true;
    }
}
