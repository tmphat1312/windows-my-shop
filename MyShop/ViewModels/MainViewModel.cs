using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Core.Services;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Windows.Foundation.Metadata;

namespace MyShop.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    private readonly IStatisticDataService _statisticDataService;

    [ObservableProperty]
    private bool isLoading = true;

    [ObservableProperty]
    private bool isContentReady = false;

    [ObservableProperty]
    private int countSellingBooks = 0;

    [ObservableProperty]
    private int countNewOrders = 0;

    [ObservableProperty]
    private int countBooks = 0;

    [ObservableProperty]
    private string selectedType = "day";

    [ObservableProperty]
    private DateTime startDate = DateTime.Now;

    [ObservableProperty]
    private DateTime lastDate = DateTime.Now;

    public RelayCommand LoadDataRangeDateCommand;

    [ObservableProperty]
    private string errorMessage = "Please select date range from 1 to 30 days";

    private PlotModel _revenue_ProfitGraph;

    public PlotModel Revenue_ProfitGraph
    {
    
        get => _revenue_ProfitGraph;
        set
        {
            if (_revenue_ProfitGraph != value)
            {
                _revenue_ProfitGraph = value;
                OnPropertyChanged(nameof(Revenue_ProfitGraph));
            }
        }
            
    }

    [ObservableProperty]
    public Visibility pickRangeDate = Visibility.Visible;

    [ObservableProperty]
    public Visibility grapVisibility = Visibility.Collapsed;

    [ObservableProperty]
    public IEnumerable<Revenue_Profit> revenue_Profits;

    [ObservableProperty]
    public IEnumerable<BookSaleStat> bookSaleStats;



    public MainViewModel(IStatisticDataService statisticDataService)
    {
        _statisticDataService = statisticDataService;
        LoadDataAsync();
        LoadDataRangeDateCommand = new RelayCommand(ExecuteLoadDataRangeDateCommand, CanExecuteLoadDataRangeDateCommand);
    }

    partial void OnRevenue_ProfitsChanged(IEnumerable<Revenue_Profit> value)
    {
        var Graph = new PlotModel { Title = "Revenue & Profit Graph" };

        var categoryAxis = new CategoryAxis { Position = AxisPosition.Left, Maximum = value.Count() };

        foreach (var item in value)
        {        
           categoryAxis.Labels.Add(item.Date);
        }


        Graph.Axes.Add(categoryAxis);

        double maxValue = 0;

        foreach(var item in value)
        {
            double minRevenue = value.Max(item => item.Revenue) / 100000;
            double minProfit = value.Max(item => item.Profit) / 100000;
            maxValue = Math.Max(minRevenue, minProfit);
        }

        var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, AbsoluteMinimum = 0,Maximum=maxValue*1.1 };
        Graph.Axes.Add(valueAxis);

        // Tạo BarSeries cho doanh thu và lợi nhuận
        var barSeries1 = new BarSeries { Title = "Revenue", FillColor = OxyColors.Blue };
        foreach (var item in value)
        {
            barSeries1.Items.Add(new BarItem { Value = item.Revenue/100000 });
        }
     

        var barSeries2 = new BarSeries { Title = "Profit", FillColor = OxyColors.Green };
        foreach (var item in value)
        {
            barSeries2.Items.Add(new BarItem { Value = item.Profit/100000 });
        }

        Graph.Series.Add(barSeries1);
        Graph.Series.Add(barSeries2);

        Graph.IsLegendVisible = true;


        Revenue_ProfitGraph = Graph;
    }

    partial void OnSelectedTypeChanged(string value)
    {
       LoadDataAsync();

        if(value == "day")
        {
        
                     PickRangeDate = Visibility.Visible;
                    GrapVisibility = Visibility.Collapsed;
         }
         else
        {
        
                     PickRangeDate = Visibility.Collapsed;
                   
          }
    }

    public async Task LoadDataAsync()
    {
    
        IsLoading = true;
        IsContentReady = false;

        await Task.Run(async () => await _statisticDataService.LoadDataAsync(SelectedType));

        var (countSellingBooks, _, _) = _statisticDataService.CountSellingBooksAsync(SelectedType);
        var (countNewOrders, _, _) = _statisticDataService.CountNewOrdersAsync(SelectedType);
        var (countBooks, _, _) = _statisticDataService.CountBooksAsync();

          if (countSellingBooks != null)
           {
               CountSellingBooks = countSellingBooks;
           }

           if(countNewOrders != null)
           {
               CountNewOrders = countNewOrders;
           }

           if(countBooks != null) { 
            
             CountBooks = countBooks;
           }
            
          UpdateDataGraph();

           IsLoading = false;
           IsContentReady = true;
    }

    public async void UpdateDataGraph()
    {
        if (SelectedType != "day")
        {
          var (returnList,message,error) =  await Task.Run(async () => await _statisticDataService.GetRevenue_ProfitAsync(SelectedType));

            if(returnList != null)
            {
                 Revenue_Profits = returnList;
                GrapVisibility = Visibility.Visible;
            }

            var (returnList2, message2, error2) = await _statisticDataService.GetBookSaleStatAsync(SelectedType);
            if (returnList2 != null)
            {
                BookSaleStats = returnList2;
            }
        }
    }

    public bool CanExecuteLoadDataRangeDateCommand()
    {
        ErrorMessage = string.Empty;

        TimeSpan timeSpan = LastDate - StartDate;

        if (timeSpan.Days > 0 && timeSpan.Days <= 30)
        {
            return true;
          
        }
        ErrorMessage = "Please select date range from 1 to 30 days";
        return false;
    }

    public async void ExecuteLoadDataRangeDateCommand()
    {

        string date1 = StartDate.ToString("yyyy-MM-dd");
        string date2 = LastDate.ToString("yyyy-MM-dd");

        string query = $"dateRange&startDate={date1}&endDate={date2}";

        var (returnList, message, error) = await _statisticDataService.GetRevenue_ProfitAsync(query);

        if (returnList != null)
        {
            Revenue_Profits = returnList;
        }

        var (returnList2, message2, error2) = await _statisticDataService.GetBookSaleStatAsync(query);
        if (returnList2 != null)
        {
            BookSaleStats = returnList2;
        }

        GrapVisibility = Visibility.Visible;
    }

    partial void OnStartDateChanged(DateTime value)
    {
        LoadDataRangeDateCommand.NotifyCanExecuteChanged();
    }

    partial void OnLastDateChanged(DateTime value)
    {
        LoadDataRangeDateCommand.NotifyCanExecuteChanged();
    }
}
