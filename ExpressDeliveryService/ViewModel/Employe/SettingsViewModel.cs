using Common;
using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Employe
{
    internal class SettingsViewModel : BaseViewModel
    {
        internal SettingsViewModel() { }

        internal SettingsViewModel(EmployeModel activeUser)
        {
            InitializeRepositories();

            _currentUser = _employeRepository.FindById(activeUser.Id);

            InitializeCommand();
            SetSettingsUserFields();
        }

        private readonly EmployeModel _currentUser;

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

        public string FavouriteLocation
        {
            get => _favouriteLocation;
            set
            {
                _favouriteLocation = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(FavouriteLocation));
            }
        }

        private string _favouriteLocation;

        #endregion

        #region Commands

        public ICommand SaveSettingsCommand { get; private set; }

        public ICommand ShowMapCommand { get; private set; }

        #endregion

        #region Repositories

        private IGenericRepository<EmployeModel> _employeRepository;

        #endregion

        #region Command Methods

        private bool CanExecuteSaveSettings(object obj) =>
            StringHelper.StrIsNotNullOrWhiteSpace(Login, Name, Surname,
                Patronymic, FavouriteLocation) && _currentUser != null;

        private void ExecuteSaveSettings(object obj)
        {
            var user = EmployeModel.CreateBuilder()
                .SetId(_currentUser.Id)
                .SetLogin(Login)
                .SetPassword(_currentUser.Password)
                .SetName(Name)
                .SetSurname(Surname)
                .SetPatronymic(Patronymic)
                .SetFavouriteLocation(FavouriteLocation);

            _employeRepository.Update(user);

            MessageBox.Show(messageBoxText: "Данные были обновлены", caption: "Успех",
               button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private async void ExecuteShowMap(object obj) =>
            await (Application.Current as App).DisplayWindow
            .ShowDialog(viewModel: new MapPopupViewModel(streetSource: obj as string));

        private bool CanExecuteShowMap(object obj) =>
            !string.IsNullOrWhiteSpace(obj as string);

        #endregion

        #region Other Methods

        private void InitializeCommand()
        {
            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
                canExecuteFunc: CanExecuteShowMap);

            SaveSettingsCommand = new RelayCommand(executeAction: ExecuteSaveSettings,
                canExecuteFunc: CanExecuteSaveSettings);
        }

        private void InitializeRepositories()
        {
            _employeRepository = new EFGenericRepository<EmployeModel>();
        }

        private void SetSettingsUserFields()
        {
            Name = _currentUser.Name;
            Surname = _currentUser.Surname;
            Patronymic = _currentUser.Patronymic;
            Mail = _currentUser.Mail;
            Login = _currentUser.Login;
            FavouriteLocation = _currentUser.FavouriteLocation;
        }

        #endregion
    }
}
