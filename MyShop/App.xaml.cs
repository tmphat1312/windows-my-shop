using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

using MyShop.Activation;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Http;
using MyShop.Core.Repository;
using MyShop.Core.Services;
using MyShop.Helpers;
using MyShop.Models;
using MyShop.Services;
using MyShop.ViewModels;
using MyShop.Views;

namespace MyShop;

public partial class App : Application
{
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar
    {
        get; set;
    }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Repositories
            services.AddHttpClient<IUserRepository, UserRepository>();

            // Http clients
            services.AddHttpClient("Backend", client =>
            {
                var host = App.GetService<IStoreServerOriginService>().Host;
                var port = App.GetService<IStoreServerOriginService>().Port;

                client.BaseAddress = new Uri(@$"{host}:{port}/api/v1/");
            }).AddHttpMessageHandler<AccessTokenHandler>().AddHttpMessageHandler<AuthenticationResponseHandler>();

            // Http handlers
            services.AddTransient<AccessTokenHandler>();
            services.AddTransient<AuthenticationResponseHandler>();

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();
            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ISampleDataService, SampleDataService>();
            services.AddSingleton<IUserDataService, UserDataService>();
            services.AddSingleton<IBookDataService, BookDataService>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IReviewDataService, ReviewDataService>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IOrderDataService, OrderDataService>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<ICategoryDataService, CategoryDataService>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IStoreLoginCredentialsService, StoreCredentialsService>();
            services.AddSingleton<IStoreServerOriginService, StoreServerOriginService>();
            services.AddSingleton<IStorePageSettingsService, StorePageSettingsService>();
            services.AddSingleton<IStatisticDataService, StatisticDataService>();
            services.AddSingleton<IStatisticRepository, StatisticRepository>();

            // Views and ViewModels
            services.AddTransient<ImportDataViewModel>();
            services.AddTransient<ImportDataPage>();
            services.AddTransient<AddCategoryViewModel>();
            services.AddTransient<AddCategoryPage>();
            services.AddTransient<CategoryDetailControlViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<CategoryPage>();
            services.AddTransient<AddOrderViewModel>();
            services.AddTransient<AddOrderPage>();
            services.AddTransient<OrderDetailControlViewModel>();
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<OrdersPage>();
            services.AddTransient<AddUserViewModel>();
            services.AddTransient<AddUserPage>();
            services.AddTransient<AddBookViewModel>();
            services.AddTransient<AddBookPage>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<AccountPage>();
            services.AddTransient<BooksDetailViewModel>();
            services.AddTransient<BooksDetailPage>();
            services.AddTransient<BooksViewModel>();
            services.AddTransient<BooksPage>();
            services.AddTransient<UsersViewModel>();
            services.AddTransient<UsersPage>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();
            services.AddTransient<LoginControlViewModel>();


            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        await App.GetService<IActivationService>().ActivateAsync(args);
    }
}
