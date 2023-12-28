using Microsoft.UI.Xaml.Controls;
using MyShop.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyShop.Views;
public sealed partial class LoginControl : UserControl
{

    public LoginControlViewModel ViewModel
    {
        get;
    }

    public LoginControl()
    {
        InitializeComponent();
        ViewModel = App.GetService<LoginControlViewModel>();
    }

    private void LoginButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.Email = LoginEmailBox.Text;
        ViewModel.Password = LoginPasswordBox.Password;
        ViewModel.IsRemembered = RememberMeCheckBox.IsChecked ?? false;
        ViewModel.LoginAsync();
    }

    private async void Login_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var (_, _) = await ViewModel.GetStoredCredentialsAsync();
        var (_, _) = await ViewModel.GetStoredServerOriginAsync();

        RememberMeCheckBox.IsChecked = ViewModel.IsRemembered;
        LoginEmailBox.Text = ViewModel.Email;
        LoginPasswordBox.Password = ViewModel.Password;
        ServerHostBox.Text = ViewModel.ServerHost;
        ServerPortBox.Value = ViewModel.ServerPort;
    }
}
