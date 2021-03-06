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

        public string Name
        {
            get => _name;
            set
            {
                _name = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _name;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _surname;

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        private string _patronymic;

        public string Mail
        {
            get => _mail;
            set
            {
                _mail = Regex.IsMatch(input: value,
                    pattern: "^([a-z0-9_-]+\\.)*[a-z0-9_-]+@[a-z0-9_-]+(\\.[a-z0-9_-]+)*\\.[a-z]{2,6}$")
                    ? value
                    : string.Empty;
                OnPropertyChanged(Mail);
            }
        }

        private string _mail;

        public string Login
        {
            get => _login;
            set
            {
                _login = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _login;

        #endregion

        #region Repositories

        private IGenericRepository<UserModel> _userRepository;

        #endregion

        #region Commands

        public ICommand UpdateUserDataCommand { get; private set; }

        #endregion

        #region Command Logic

        private bool CanExecuteUpdateUserData(object obj) =>
            StringHelper.StrIsNotNullOrWhiteSpace(Name, Surname,
                Mail, Login) && !(_currentUser is null);

        private void ExecuteUpdateUserData(object obj)
        {
            var user = UserModel.CreateBuilder()
                .SetId(_currentUser.Id)
                .SetName(Name)
                .SetSurname(Surname)
                .SetPatronymic(Patronymic)
                .SetMail(Mail)
                .SetPassword(_currentUser.Password)
                .SetLogin(Login);

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
            Name = _currentUser.Name;
            Surname = _currentUser.Surname;
            Patronymic = _currentUser.Patronymic;
            Mail = _currentUser.Mail;
            Login = _currentUser.Login;
        }
    }
}
