using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyShop.Controls;

public sealed partial class PaginationControl : UserControl
{
    public PaginationControl()
    {
        InitializeComponent();
        Loaded += PaginationControl_Loaded;
        SizeChanged += PaginationControl_SizeChanged;
    }

    private void PaginationControl_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateVisualState();
    }

    private void PaginationControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        var windowWidth = App.MainWindow.Width;

        if (windowWidth < 640)
        {
            PagingTextFull.Visibility = Visibility.Collapsed;
            PagingTextCompact.Visibility = Visibility.Visible;
        }
        else
        {
            PagingTextFull.Visibility = Visibility.Visible;
            PagingTextCompact.Visibility = Visibility.Collapsed;
        }
    }

    public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register(
        nameof(CurrentPage),
        typeof(int),
        typeof(PaginationControl),
        new PropertyMetadata(1, OnCurrentPagePropertyChanged)
    );

    public int CurrentPage
    {
        get => (int)GetValue(CurrentPageProperty);
        set => SetValue(CurrentPageProperty, value);
    }

    private static void OnCurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Handle property changed logic here if needed
    }

    public static readonly DependencyProperty TotalPagesProperty = DependencyProperty.Register(
        nameof(TotalPages),
        typeof(int),
        typeof(PaginationControl),
        new PropertyMetadata(1, OnTotalPagesPropertyChanged)
    );

    public int TotalPages
    {
        get => (int)GetValue(TotalPagesProperty);
        set => SetValue(TotalPagesProperty, value);
    }

    private static void OnTotalPagesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Handle property changed logic here if needed
    }

    public static readonly DependencyProperty ItemsPerPageProperty = DependencyProperty.Register(
        nameof(ItemsPerPage),
        typeof(int),
        typeof(PaginationControl),
        new PropertyMetadata(1, OnItemsPerPagePropertyChanged)
    );

    public int ItemsPerPage
    {
        get => (int)GetValue(ItemsPerPageProperty);
        set => SetValue(ItemsPerPageProperty, value);
    }

    private static void OnItemsPerPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Handle property changed logic here if needed
    }

    public static readonly DependencyProperty TotalItemsProperty = DependencyProperty.Register(
        nameof(TotalItems),
        typeof(int),
        typeof(PaginationControl),
        new PropertyMetadata(1, OnTotalItemsPropertyChanged)
    );

    public int TotalItems
    {
        get => (int)GetValue(TotalItemsProperty);
        set => SetValue(TotalItemsProperty, value);
    }

    private static void OnTotalItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Handle property changed logic here if needed
    }
}
