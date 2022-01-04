using Common;
using Data.Repositories;
using Data.Repositories.Abstract;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class SettingsViewModel : BaseViewModel
    {
        internal SettingsViewModel() { }

        internal SettingsViewModel(UserModel activeUser)
        {
            InitializeRepositories();

            _currentUser = _userRepository
                .Get(user => user.Id == activeUser.Id)
                .First();

            InitializeCommands();
            SetSettingsUserFields();
        }

        private readonly UserModel _currentUser;

        #region Properties

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _userName;

        public string UserSurname
        {
            get => _userSurname;
            set
            {
                _userSurname = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(UserSurname));
            }
        }

        private string _userSurname;

        public string UserPatronymic
        {
            get => _userPatronymic;
            set
            {
                _userPatronymic = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(UserPatronymic));
            }
        }

        private string _userPatronymic;

        public string UserMail
        {
            get => _userMail;
            set
            {
                _userMail = Regex.IsMatch(input: value,
                    pattern: "^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$")
                    ? value
                    : string.Empty;
                OnPropertyChanged(UserMail);
            }
        }

        private string _userMail;

        public string UserLogin
        {
            get => _userLogin;
            set
            {
                _userLogin = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(UserLogin));
            }
        }

        private string _userLogin;

        #endregion

        #region Repositories

        private IGenericRepository<UserModel> _userRepository;

        #endregion

        #region Commands

        public ICommand UpdateUserDataCommand { get; private set; }

        #endregion

        #region Command Logic

        private bool CanExecuteUpdateUserData(object obj) =>
            StringHelper.StrIsNotNullOrWhiteSpace(UserName, UserSurname,
                UserMail, UserLogin) && !(_currentUser is null);

        private void ExecuteUpdateUserData(object obj)
        {
            var user = UserModel.CreateBuilder()
                .SetId(_currentUser.Id)
                .SetName(UserName)
                .SetSurname(UserSurname)
                .SetPatronymic(UserPatronymic)
                .SetMail(UserMail)
                .SetPassword(_currentUser.Password)
                .SetLogin(UserLogin);

            _userRepository.Update(user);

            MessageBox.Show(messageBoxText: "Данные были обновлены", caption: "Успех",
                button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        #endregion

        private void InitializeCommands()
        {
            UpdateUserDataCommand = new RelayCommand(executeAction: ExecuteUpdateUserData,
                canExecuteFunc: CanExecuteUpdateUserData);
        }

        private void InitializeRepositories()
        {
            _userRepository = new EFGenericRepository<UserModel>();
        }

        private void SetSettingsUserFields()
        {
            UserName = _currentUser.Name;
            UserSurname = _currentUser.Surname;
            UserPatronymic = _currentUser.Patronymic;
            UserMail = _currentUser.Mail;
            UserLogin = _currentUser.Login;
        }
    }
}
