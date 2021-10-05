using ExpressDeliveryService.Data;
using ExpressDeliveryService.Model;
using ExpressDeliveryService.Services.Command;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    public sealed class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            InitialEnterFields();
            InitialRegisterFields();
            InitialCommands();
        }

        #region Properties

        private readonly DataManager _db = new DataManager(DataManager.ActualNameDB);

        #region Enter Fields

        public string EnterUserLogin
        {
            get => _enterUserLogin;
            set
            {
                if (value != String.Empty)
                {
                    _enterUserLogin = value;
                    OnPropertyChanged("EnterUserLogin");
                }
                else
                {
                    _enterUserLogin = String.Empty;
                    OnPropertyChanged("EnterUserLogin");
                }
            }
        }

        private string _enterUserLogin;

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

        public unsafe string Password
        {
            [SecurityCritical]
            get
            {
                if (SecurePassword != null)
                {
                    SecureString securePassword = SecurePassword;
                    IntPtr intPtr = Marshal.SecureStringToBSTR(securePassword);
                    return new string((char*)(void*)intPtr);
                }
                return String.Empty;
            }
            [SecurityCritical]
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                using (SecureString secureString = new SecureString())
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        secureString.AppendChar(value[i]);
                    }
                }
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

        #region Registration Fields

        public string CreateUserLogin
        {
            get => _createUserLogin;
            set
            {
                if (value != String.Empty)
                {
                    _createUserLogin = value;
                    OnPropertyChanged("CreateUserLogin");
                }
                else
                {
                    _createUserLogin = String.Empty;
                    OnPropertyChanged("CreateUserLogin");
                }
            }
        }

        private string _createUserLogin;

        public string CreateUserName
        {
            get => _createUserName;
            set
            {
                if (value != String.Empty)
                {
                    _createUserName = value;
                    OnPropertyChanged("CreateUserName");
                }
                else
                {
                    _createUserName = String.Empty;
                    OnPropertyChanged("CreateUserName");
                }
            }
        }

        private string _createUserName;

        public string CreateUserSurname
        {
            get => _createUserSurname;
            set
            {
                if (value != String.Empty)
                {
                    _createUserSurname = value;
                    OnPropertyChanged("CreateUserSurname");
                }
                else
                {
                    _createUserSurname = String.Empty;
                    OnPropertyChanged("CreateUserSurname");
                }
            }
        }

        private string _createUserSurname;

        public string CreateUserPatronymic
        {
            get => _createUserPatronymic;
            set
            {
                if (value != String.Empty)
                {
                    _createUserPatronymic = value;
                    OnPropertyChanged("CreateUserPatronymic");
                }
                else
                {
                    _createUserPatronymic = String.Empty;
                    OnPropertyChanged("CreateUserPatronymic");
                }
            }
        }

        private string _createUserPatronymic;

        public string CreateUserMail
        {
            get => _createUserMail;
            set
            {
                if (value != String.Empty)
                {
                    _createUserMail = value;
                    OnPropertyChanged("CreateUserMail");
                }
                else
                {
                    _createUserMail = String.Empty;
                    OnPropertyChanged("CreateUserMail");
                }
            }
        }

        private string _createUserMail;

        #region RegisterPasswordBox

        public SecureString SecureRegisterPassword
        {
            get => _secureRegisterPassword;
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

        public ICommand EnterButtonClickCommand { get; private set; }

        public ICommand RegisterButtonClickCommand { get; private set; }

        public ICommand ExitButtonClickCommand { get; private set; }

        public ICommand RecoverButtonClickCommand { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Commands

        #region EnterButtonClickCommand

        private void EnterButtonClick(object obj)
        {
            var collection = new MongoClient().GetDatabase(DataManager.ActualNameDB)
                .GetCollection<BsonDocument>("Users");

            var filter = new BsonDocument("$and", new BsonArray{

                new BsonDocument("Login",EnterUserLogin),
                new BsonDocument("Password", Password)
            });

            var people = collection.Find(filter).ToList();

            if (people.Count > 0)
            {
                var view = obj as LoginView;

                var displayRootRegistry = (Application.Current as App).DisplayWindow;

                var user = new User();

                user = BsonSerializer.Deserialize<User>(people[0]);

                displayRootRegistry.Show(new MainViewModel(user));

                view.Close();
            }

        }

        #endregion

        #region RegisterButtonClickCommand

        private void RegisterButtonClick(object obj)
        {
            var newUser = new User()
            {
                Id = new Guid(),
                Name = CreateUserName,
                Surname = CreateUserSurname,
                Patronymic = CreateUserPatronymic,
                Mail = CreateUserMail,
                Login = CreateUserLogin,
                Password = RegisterPassword,
                Orders = null
            };

            _db.Create("Users", newUser);

            var view = obj as LoginView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new MainViewModel(newUser));

            view.Close();
        }

        #endregion

        #region ExitButtonClickCommand

        private static void ExitButtonClick(object obj) => Application.Current.Shutdown();

        #endregion

        #region RecoverButtonClickCommand

        private static void RecoverButtonClick(object obj)
        {

        }

        #endregion

        #endregion

        private void InitialEnterFields()
        {
            IsPasswordWatermarkVisible = true;
        }

        private void InitialRegisterFields()
        {
            IsRegisterPasswordWatermarkVisible = true;
        }

        private void InitialCommands()
        {
            EnterButtonClickCommand = new DelegateCommandService(EnterButtonClick);
            RegisterButtonClickCommand = new DelegateCommandService(RegisterButtonClick);
            ExitButtonClickCommand = new DelegateCommandService(ExitButtonClick);
            RecoverButtonClickCommand = new DelegateCommandService(RecoverButtonClick);
        }

        #endregion

    }
}
