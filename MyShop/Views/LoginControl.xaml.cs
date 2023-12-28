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
        ViewModel.LoginAsync();
    }
}
