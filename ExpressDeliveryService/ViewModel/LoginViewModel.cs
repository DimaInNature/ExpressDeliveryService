using ExpressDeliveryService.Service.Command;
using ExpressDeliveryService.View;
using System.ComponentModel;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginViewModel()
        {
            IsPasswordWatermarkVisible = true;
            IsRegisterPasswordWatermarkVisible = true;

            EnterButtonClickCommand = new DelegateCommandService(EnterButtonClick);
            RegisterButtonClickCommand = new DelegateCommandService(RegisterButtonClick);
            ExitButtonClickCommand = new DelegateCommandService(ExitButtonClick);
            RecoverButtonClickCommand = new DelegateCommandService(RecoverButtonClick);
        }

        #region Properties

        #region AuthorizationPasswordBox

        public SecureString SecurePassword
        {
            get { return _securePassword; }
            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        private SecureString _securePassword;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                IsPasswordWatermarkVisible = string.IsNullOrEmpty(_password);
            }
        }

        private string _password;

        public bool IsPasswordWatermarkVisible
        {
            get { return _isPasswordWatermarkVisible; }
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
            get { return _secureRegisterPassword; }
            set
            {
                _secureRegisterPassword = value;
                OnPropertyChanged("SecureRegisterPassword");
            }
        }

        private SecureString _secureRegisterPassword;

        public string RegisterPassword
        {
            get { return _registerPassword; }
            set
            {
                _registerPassword = value;
                IsRegisterPasswordWatermarkVisible = string.IsNullOrEmpty(_registerPassword);
            }
        }

        private string _registerPassword;

        public bool IsRegisterPasswordWatermarkVisible
        {
            get { return _isRegisterPasswordWatermarkVisible; }
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

        public ICommand EnterButtonClickCommand { get; private set; }

        private void EnterButtonClick(object obj)
        {
            // Параметр является ссылкой на представление
            var view = obj as LoginView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new MainViewModel());

            view.Close();
        }

        #endregion

        #region RegisterButtonClickCommand

        public ICommand RegisterButtonClickCommand { get; private set; }

        private void RegisterButtonClick(object obj)
        {
            
        }

        #endregion

        #region ExitButtonClickCommand

        public ICommand ExitButtonClickCommand { get; private set; }

        private void ExitButtonClick(object obj) => Application.Current.Shutdown();

        #endregion

        #region RecoverButtonClickCommand

        public ICommand RecoverButtonClickCommand { get; private set; }

        private void RecoverButtonClick(object obj)
        {

        }

        #endregion

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion
    }
}
