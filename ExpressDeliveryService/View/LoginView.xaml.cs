using ExpressDeliveryService.ViewModel;
using System.Windows;

namespace ExpressDeliveryService.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private LoginViewModel ViewModel
        {
            get { return (LoginViewModel)DataContext; }
        }

        private void EnterPasswordPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = EnterPasswordPasswordBox.Password;
            ViewModel.SecurePassword = EnterPasswordPasswordBox.SecurePassword;
        }

        private void RegisterPasswordPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.RegisterPassword = RegisterPasswordPasswordBox.Password;
            ViewModel.SecureRegisterPassword = RegisterPasswordPasswordBox.SecurePassword;
        }
    }
}
