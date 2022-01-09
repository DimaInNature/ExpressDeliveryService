using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.View;
using Models;
using Models.Enums;
using MVVM.Command;
using MVVM.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class MainViewModel : BaseViewModel
    {
        internal MainViewModel() { }

        internal MainViewModel(UserModel activeUser)
        {
            _activeUser = activeUser;

            InitializeRepositories();
            InitializeData();
            InitializeCommands();
            SetViewCondition();
        }

        #region Properties

        private readonly UserModel _activeUser;

        #region Data

        public List<OrderModel> Orders
        {
            get => _orders is null
                ? new List<OrderModel>()
                : _orders;
            set
            {
                _orders = value is null
                    ? new List<OrderModel>()
                    : value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private List<OrderModel> _orders;

        public OrderModel SelectedUpdateOrder
        {
            get => _selectedUpdateOrder;
            set
            {
                _selectedUpdateOrder = value;
                OnPropertyChanged(nameof(SelectedUpdateOrder));
            }
        }

        private OrderModel _selectedUpdateOrder;

        public OrderModel SelectedDeleteOrder
        {
            get => _selectedDeleteOrder;
            set
            {
                _selectedDeleteOrder = value;
                OnPropertyChanged(nameof(SelectedDeleteOrder));
            }
        }

        private OrderModel _selectedDeleteOrder;

        #endregion

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

        #region Repositories

        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #endregion

        #region Commands

        public ICommand LogoutCommand { get; private set; }

        public ICommand SelectUpdateItemCommand { get; private set; }

        public ICommand SelectDeleteItemCommand { get; private set; }

        public ICommand ShowUpdatePageCommand { get; private set; }

        public ICommand ShowDeletePageCommand { get; private set; }

        public ICommand ShowViewPageCommand { get; private set; }

        public ICommand ShowCreatePageCommand { get; private set; }

        public ICommand ShowSettingsPageCommand { get; private set; }

        public ICommand ShowMenuPageCommand { get; private set; }

        public ICommand UpdateDataCollectionCommand { get; private set; }

        public ICommand CloseApplicationCommand { get; private set; }

        #endregion

        #region Command Logic

        private static void CloseApplication(object obj) =>
           Application.Current.Shutdown();

        private void UpdateDataCollection(object obj) => InitializeData();

        private void Logout(object obj) =>
            (Application.Current as App).DisplayWindow
            .Show(viewModel: new LoginViewModel(), closableView: obj as MainView);

        private bool CanExecuteShowMenuPage(object obj) =>
            !(FrameSource is null && MenuIsVisible == Visibility.Visible);

        private void ExecuteShowMenuPage(object obj)
        {
            FrameSource = null;

            MenuIsVisible = Visibility.Visible;
        }

        private bool CanExecuteSelectUpdateItem(object obj) =>
            !(SelectedUpdateOrder is null);

        private void ExecuteSelectUpdateItem(object obj)
        {
            FrameSource = new UpdateOrderView(viewModel: new
                UpdateOrderViewModel(updatebleOrder: SelectedUpdateOrder));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteSelectDeleteItem(object obj) =>
            !(SelectedDeleteOrder is null);

        private void ExecuteSelectDeleteItem(object obj)
        {
            FrameSource = new DeleteOrderView(viewModel:
                new DeleteOrderViewModel(deletebleOrder: SelectedDeleteOrder));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowViewPage(object obj) =>
            !(FrameSource is ReadOrderView);

        private void ExecuteShowViewPage(object obj)
        {
            FrameSource = new ReadOrderView(viewModel:
                new ReadOrderViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowCreatePage(object obj) =>
            !(FrameSource is CreateOrderView);

        private void ExecuteShowCreatePage(object obj)
        {
            FrameSource = new CreateOrderView(viewModel:
                new CreateOrderViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteShowUpdatePage(object obj) =>
            !(FrameSource is UpdateOrderView);

        private void ExecuteShowUpdatePage(object obj)
        {
            var order = Orders.FirstOrDefault();

            if (order != null)
            {
                FrameSource = new UpdateOrderView(viewModel:
                    new UpdateOrderViewModel(updatebleOrder: order));

                MenuIsVisible = Visibility.Collapsed;
            }
            else
                MessageBox.Show(messageBoxText: "Отсутствуют заказы для изменения", caption: "Ошибка",
                    button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private bool CanExecuteShowDeletePage(object obj) =>
            !(FrameSource is DeleteOrderView);

        private void ExecuteShowDeletePage(object obj)
        {
            var order = Orders.FirstOrDefault();

            if (order != null)
            {
                FrameSource = new DeleteOrderView(viewModel:
                    new DeleteOrderViewModel(deletebleOrder: order));

                MenuIsVisible = Visibility.Collapsed;
            }
            else
                MessageBox.Show(messageBoxText: "Отсутствуют заказы для удаления", caption: "Ошибка",
                    button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private bool CanExecuteShowSettingsPage(object obj) =>
            !(FrameSource is SettingsView);

        private void ExecuteShowSettingsPage(object obj)
        {
            FrameSource = new SettingsView(viewModel:
                new SettingsViewModel(activeUser: _activeUser));

            MenuIsVisible = Visibility.Collapsed;
        }

        #endregion

        #region Other Logic

        private void InitializeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
        }

        private void InitializeCommands()
        {
            SelectUpdateItemCommand = new RelayCommand(executeAction: ExecuteSelectUpdateItem,
                canExecuteFunc: CanExecuteSelectUpdateItem);

            SelectDeleteItemCommand = new RelayCommand(executeAction: ExecuteSelectDeleteItem,
                canExecuteFunc: CanExecuteSelectDeleteItem);

            ShowViewPageCommand = new RelayCommand(executeAction: ExecuteShowViewPage,
                canExecuteFunc: CanExecuteShowViewPage);

            ShowUpdatePageCommand = new RelayCommand(executeAction: ExecuteShowUpdatePage,
                canExecuteFunc: CanExecuteShowUpdatePage);

            ShowDeletePageCommand = new RelayCommand(executeAction: ExecuteShowDeletePage,
                canExecuteFunc: CanExecuteShowDeletePage);

            ShowCreatePageCommand = new RelayCommand(executeAction: ExecuteShowCreatePage,
                canExecuteFunc: CanExecuteShowCreatePage);

            ShowSettingsPageCommand = new RelayCommand(executeAction: ExecuteShowSettingsPage,
                canExecuteFunc: CanExecuteShowSettingsPage);

            ShowMenuPageCommand = new RelayCommand(executeAction: ExecuteShowMenuPage,
                canExecuteFunc: CanExecuteShowMenuPage);

            UpdateDataCollectionCommand = new RelayCommand(UpdateDataCollection);

            LogoutCommand = new RelayCommand(Logout);

            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        private void InitializeData()
        {
            Orders = _activeUser.Role == UserRole.Admin
                ? _orderRepository.Get().ToList()
                : _orderRepository.Get(order => order.UserId == _activeUser.Id).ToList();
        }

        private void SetViewCondition() =>

            /* For convenience.
               To remember that we are in the debag mode. */
#if DEBUG
            _viewTitle = $"{_activeUser.Login} DEBUG MODE";

#else
            _viewTitle = _activeUser.Login;
#endif

        #endregion
    }
}
