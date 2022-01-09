using Common;
using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Employe;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class LoginViewModel : BaseViewModel
    {
        internal LoginViewModel()
        {
            InitializeRepositories();
            SetViewCondition();
            InitializeCommands();
        }

        #region Properties

        #region Login

        public string EnterUserLogin
        {
            get => _enterUserLogin;
            set
            {
                _enterUserLogin = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(EnterUserLogin));
            }
        }

        private string _enterUserLogin;

        public SecureString SecurePassword
        {
            get => _securePassword;
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(SecurePassword));
                OnPropertyChanged(nameof(Password));
            }
        }

        private SecureString _securePassword;

        public unsafe string Password
        {
            [SecurityCritical]
            get => SecurePassword is null
                ? string.Empty
                : new string(value: (char*)(void*)Marshal.SecureStringToBSTR(s: SecurePassword));
            [SecurityCritical]
            set
            {
                if (value is null)
                    value = string.Empty;

                using (SecureString secureString = new SecureString())
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        secureString.AppendChar(c: value[i]);
                    }
                }
                _password = value;
                IsPasswordWatermarkVisible = string.IsNullOrEmpty(_password);
            }
        }

        private string _password;

        #endregion

        #region Registration

        public string CreateUserLogin
        {
            get => _createUserLogin;
            set
            {
                _createUserLogin = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(CreateUserLogin));
            }
        }

        private string _createUserLogin;

        public string CreateUserName
        {
            get => _createUserName;
            set
            {
                _createUserName = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(CreateUserName));
            }
        }

        private string _createUserName;

        public string CreateUserSurname
        {
            get => _createUserSurname;
            set
            {
                _createUserSurname = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(CreateUserSurname));
            }
        }

        private string _createUserSurname;

        public string CreateUserPatronymic
        {
            get => _createUserPatronymic;
            set
            {
                _createUserPatronymic = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(CreateUserPatronymic));
            }
        }

        private string _createUserPatronymic;

        public string CreateUserMail
        {
            get => _createUserMail;
            set
            {
                _createUserMail = Regex.IsMatch(input: value,
                    pattern: "^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$")
                    ? value
                    : string.Empty;
                OnPropertyChanged(nameof(CreateUserMail));
            }
        }

        private string _createUserMail;

        public bool CreateUserIsEmploye
        {
            get => _createUserIsEmploye;
            set
            {
                _createUserIsEmploye = value;
                OnPropertyChanged(nameof(CreateUserIsEmploye));
            }
        }

        private bool _createUserIsEmploye;

        #region RegisterPasswordBox

        public SecureString SecureRegisterPassword
        {
            get => _secureRegisterPassword;
            set
            {
                _secureRegisterPassword = value;
                OnPropertyChanged(nameof(SecureRegisterPassword));
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

        #endregion

        #endregion

        #region Commands

        public ICommand LoginCommand { get; private set; }

        public ICommand RegistrationCommand { get; private set; }

        public ICommand CloseApplicationCommand { get; private set; }

        public ICommand RecoverCommand { get; private set; }

        #endregion

        #region Repositories

        private IGenericRepository<UserModel> _userRepository;
        private IGenericRepository<EmployeModel> _employeRepository;

        #endregion

        #region View

        public bool IsPasswordWatermarkVisible
        {
            get => _isPasswordWatermarkVisible;
            set
            {
                if (value.Equals(_isPasswordWatermarkVisible)) return;
                _isPasswordWatermarkVisible = value;
                OnPropertyChanged(nameof(IsPasswordWatermarkVisible));
            }
        }

        private bool _isPasswordWatermarkVisible;

        public bool IsRegisterPasswordWatermarkVisible
        {
            get => _isRegisterPasswordWatermarkVisible;
            set
            {
                if (value.Equals(_isRegisterPasswordWatermarkVisible)) return;
                _isRegisterPasswordWatermarkVisible = value;
                OnPropertyChanged(nameof(IsRegisterPasswordWatermarkVisible));
            }
        }

        private bool _isRegisterPasswordWatermarkVisible;

        #endregion

        #endregion

        #region Commands

        private bool CanExecuteLogin(object obj) =>
            StringHelper.StrIsNotNullOrWhiteSpace(EnterUserLogin, Password);

        private void ExecuteLogin(object obj)
        {
            /* First, we look for whether there is
               a match in the table with users => */

            var existUser = _userRepository
                .Get(u => u.Login == EnterUserLogin && u.Password == Password)
                .FirstOrDefault();

            // If a match is found, window is showing and pass the link to the user =>

            if (existUser != null)
                (Application.Current as App).DisplayWindow
                    .Show(viewModel: new MainViewModel(activeUser: existUser),
                        closableView: obj as LoginView);
            else
            {
                /* If no match is found.We perform the same actions,
                   but with a table with employees. => */

                var existEmploye = _employeRepository
                    .Get(e => e.Login == EnterUserLogin && e.Password == Password)
                    .FirstOrDefault();

                if (existEmploye != null)
                    (Application.Current as App).DisplayWindow
                        .Show(viewModel: new EmployeViewModel(activeUser: existEmploye),
                        closableView: obj as LoginView);
                else
                    MessageBox.Show(messageBoxText: "Пользователя с таким логином не существует",
                        caption: "Ошибка", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }

        private bool CanExecuteRegistration(object obj) =>
            StringHelper.StrIsNotNullOrWhiteSpace(CreateUserName,
                CreateUserSurname, CreateUserMail, CreateUserLogin, RegisterPassword);

        private void ExecuteRegistration(object obj)
        {
            /* Looking for a match in the tables of users and employees.
               Because there cannot be two users with the same logins in the system. */

            var existUser = _userRepository
                .Get(u => u.Login == CreateUserLogin)
                .FirstOrDefault();

            var existEmploye = _employeRepository
                .Get(e => e.Login == CreateUserLogin)
                .FirstOrDefault();

            if (existUser is null && existEmploye is null)
            {
                if (CreateUserIsEmploye)
                {
                    var employe = EmployeModel.CreateBuilder()
                        .SetName(CreateUserName)
                        .SetSurname(CreateUserSurname)
                        .SetPatronymic(CreateUserPatronymic)
                        .SetMail(CreateUserMail)
                        .SetLogin(CreateUserLogin)
                        .SetPassword(CreateUserPatronymic);

                    _employeRepository.Create(employe);

                    (Application.Current as App).DisplayWindow
                        .Show(viewModel: new EmployeViewModel(activeUser: employe),
                            closableView: obj as LoginView);
                }
                else
                {
                    var user = UserModel.CreateBuilder()
                        .SetName(CreateUserName)
                        .SetSurname(CreateUserSurname)
                        .SetPatronymic(CreateUserPatronymic)
                        .SetMail(CreateUserMail)
                        .SetLogin(CreateUserLogin)
                        .SetPassword(RegisterPassword);

                    _userRepository.Create(user);

                    (Application.Current as App).DisplayWindow
                        .Show(viewModel: new MainViewModel(user),
                            closableView: obj as LoginView);
                }
            }
            else
                MessageBox.Show(messageBoxText: "Пользователь с таким логином уже существует",
                    caption: "Ошибка", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
        }

        private static void CloseApplication(object obj) =>
            Application.Current.Shutdown();

        #endregion

        #region Methods

        private void InitializeRepositories()
        {
            _userRepository = new EFGenericRepository<UserModel>();
            _employeRepository = new EFGenericRepository<EmployeModel>();
        }

        private void SetViewCondition()
        {
            IsPasswordWatermarkVisible = IsRegisterPasswordWatermarkVisible = true;
        }

        private void InitializeCommands()
        {
            LoginCommand = new RelayCommand(executeAction: ExecuteLogin,
                canExecuteFunc: CanExecuteLogin);

            RegistrationCommand = new RelayCommand(executeAction: ExecuteRegistration,
                canExecuteFunc: CanExecuteRegistration);

            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        #endregion
    }
}
