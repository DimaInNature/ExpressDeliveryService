using ExpressDeliveryService.Services.Command;
using ExpressDeliveryService.View;
using System.Security;
using System.Windows;
using System.Windows.Input;
using ExpressDeliveryService.ViewModel.Base;

namespace ExpressDeliveryService.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            SetUpDefaultCondition();
        }

        #region Properties

        #region AuthorizationPasswordBox

        public SecureString SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        private SecureString _securePassword;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                IsPasswordWatermarkVisible = string.IsNullOrEmpty(_password);
            }
        }

        private string _password;

        public bool IsPasswordWatermarkVisible
        {
            get => _isPasswordWatermarkVisible;
            set
            {
                if (value.Equals(_isPasswordWatermarkVisible)) return;
                _isPasswordWatermarkVisible = value;
                OnPropertyChanged("IsPasswordWatermarkVisible");
            }
        }

        private bool _isPasswordWatermarkVisible;

        #endregion

        #region RegisterPasswordBox

        public SecureString SecureRegisterPassword
        {
            get =>_secureRegisterPassword;
            set
            {
                _secureRegisterPassword = value;
                OnPropertyChanged("SecureRegisterPassword");
            }
        }

        private SecureString _secureRegisterPassword;

        public string RegisterPassword
        {
            get => _registerPassword;
            set
            {
                _registerPassword = value;
                IsRegisterPasswordWatermarkVisible = string.IsNullOrEmpty(_registerPassword);
            }
        }

        private string _registerPassword;

        public bool IsRegisterPasswordWatermarkVisible
        {
            get => _isRegisterPasswordWatermarkVisible; 
            set
            {
                if (value.Equals(_isRegisterPasswordWatermarkVisible)) return;
                _isRegisterPasswordWatermarkVisible = value;
                OnPropertyChanged("IsRegisterPasswordWatermarkVisible");
            }
        }

        private bool _isRegisterPasswordWatermarkVisible;

        #endregion

        #endregion

        #region Commands

        #region EnterButtonClickCommand

        public ICommand EnterButtonClickCommand { get; private set; } = new DelegateCommandService(EnterButtonClick);

        private static void EnterButtonClick(object obj)
        {
            // Параметр является ссылкой на представление

            var view = obj as LoginView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new MainViewModel());

            view.Close();
        }

        #endregion

        #region RegisterButtonClickCommand

        public ICommand RegisterButtonClickCommand { get; private set; } = new DelegateCommandService(RegisterButtonClick);

        private static void RegisterButtonClick(object obj)
        {
            
        }

        #endregion

        #region ExitButtonClickCommand

        public ICommand ExitButtonClickCommand { get; private set; } = new DelegateCommandService(ExitButtonClick);

        private static void ExitButtonClick(object obj) => Application.Current.Shutdown();

        #endregion

        #region RecoverButtonClickCommand

        public ICommand RecoverButtonClickCommand { get; private set; } = new DelegateCommandService(RecoverButtonClick);

        private static void RecoverButtonClick(object obj)
        {

        }

        #endregion

        #endregion

        #region Methods

        private void SetUpDefaultCondition()
        {
            IsPasswordWatermarkVisible = true;
            IsRegisterPasswordWatermarkVisible = true;
        }

        #endregion

    }
}
