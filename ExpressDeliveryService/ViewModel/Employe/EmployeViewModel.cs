using ExpressDeliveryService.View.Employe;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Employe
{
    internal class EmployeViewModel : BaseViewModel
    {
        internal EmployeViewModel() { }

        internal EmployeViewModel(EmployeModel activeUser)
        {
            _activeUser = activeUser;

            InitializeCommands();
            SetViewCondition();
        }

        private readonly EmployeModel _activeUser;

        #region Properties

        #region View

        #region Frame

        public Visibility FrameVisibility
        {
            get => _frameVisibility;
            set
            {
                _frameVisibility = value;
                OnPropertyChanged(nameof(FrameVisibility));
            }
        }

        private Visibility _frameVisibility;

        public object FrameSource
        {
            get => _frameSource;
            set
            {
                _frameSource = value;
                OnPropertyChanged(nameof(FrameSource));
            }
        }

        private object _frameSource;

        #endregion

        public Visibility MenuIsVisible
        {
            get => _menuIsVisible;
            set
            {
                _menuIsVisible = value;
                OnPropertyChanged(nameof(MenuIsVisible));
            }
        }

        private Visibility _menuIsVisible;

        public string ViewTitle
        {
            get => _viewTitle;
            set
            {
                _viewTitle = string.IsNullOrWhiteSpace(value)
                    ? "Empty"
                    : value;
                OnPropertyChanged(nameof(ViewTitle));
            }
        }

        private string _viewTitle;

        #endregion

        #endregion

        #region Commands

        public ICommand LogoutCommand { get; private set; }

        public ICommand ShowAcceptedOrdersPageCommand { get; private set; }

        public ICommand ShowNotAcceptedOrdersPageCommand { get; private set; }

        public ICommand ShowSettingsPageCommand { get; private set; }

        public ICommand ShowHomePageCommand { get; private set; }

        public ICommand ShowSelectorOrdersPageCommand { get; private set; }

        public ICommand CloseApplicationCommand { get; private set; }

        #endregion

        #region Command Logic

        private void ExecuteShowAcceptedOrdersPage(object obj)
        {
            FrameSource = new AcceptedOrdersView(viewModel:
                new AcceptedOrdersViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowAcceptedOrdersPage(object obj)
            => !(FrameSource is AcceptedOrdersView);

        private bool CanExecuteShowSelectorOrdersPage(object obj) =>
            !(FrameSource is ScrollerOrdersView);

        private void ExecuteShowSelectorOrdersPage(object obj)
        {
            FrameSource = new ScrollerOrdersView(viewModel:
                new ScrollerOrdersViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowNotAcceptedOrdersPage(object obj) =>
            !(FrameSource is NotAcceptedOrdersView);

        private void ExecuteShowNotAcceptedOrdersPage(object obj)
        {
            FrameSource = new NotAcceptedOrdersView(viewModel:
                new NotAcceptedOrdersViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowSettingsPage(object obj) =>
            !(FrameSource is SettingsView);

        private void ExecuteShowSettingsPage(object obj)
        {
            FrameSource = new SettingsView(viewModel:
                new SettingsViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowHomePage(object obj) =>
            (FrameSource != null && MenuIsVisible != Visibility.Visible);

        private void ExecuteShowHomePage(object obj)
        {
            FrameSource = null;

            MenuIsVisible = Visibility.Visible;
        }

        private void Logout(object obj) =>
            (Application.Current as App).DisplayWindow
            .Show(viewModel: new LoginViewModel(), closableView: obj as EmployeView);

        private void CloseApplication(object obj) =>
            Application.Current.Shutdown();

        #endregion

        #region Other Logic

        private void InitializeCommands()
        {
            ShowAcceptedOrdersPageCommand = new RelayCommand(executeAction: ExecuteShowAcceptedOrdersPage,
                canExecuteFunc: CanExecuteShowAcceptedOrdersPage);

            ShowNotAcceptedOrdersPageCommand = new RelayCommand(executeAction: ExecuteShowNotAcceptedOrdersPage,
                canExecuteFunc: CanExecuteShowNotAcceptedOrdersPage);

            ShowSettingsPageCommand = new RelayCommand(executeAction: ExecuteShowSettingsPage,
                canExecuteFunc: CanExecuteShowSettingsPage);

            ShowHomePageCommand = new RelayCommand(executeAction: ExecuteShowHomePage,
                canExecuteFunc: CanExecuteShowHomePage);

            ShowSelectorOrdersPageCommand = new RelayCommand(executeAction: ExecuteShowSelectorOrdersPage,
                canExecuteFunc: CanExecuteShowSelectorOrdersPage);

            CloseApplicationCommand = new RelayCommand(CloseApplication);

            LogoutCommand = new RelayCommand(Logout);
        }
        private void SetViewCondition() =>

            /* For convenience.
               To remember that we are in the debug. */
#if DEBUG
            _viewTitle = $"{_activeUser.Login} DEBUG MODE";
#else
            _viewTitle = _activeUser.Login;
#endif

        #endregion

    }
}
