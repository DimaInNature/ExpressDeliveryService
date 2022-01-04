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

        private void ShowMenuPage(object obj)
        {
            FrameSource = null;
            MenuIsVisible = Visibility.Visible;
        }

        private bool CanExecuteSelectUpdateItem(object obj) =>
            !(SelectedUpdateOrder is null);

        private void ExecuteSelectUpdateItem(object obj)
        {
            var view = new UpdateOrderView
            {
                DataContext = new UpdateOrderViewModel(updatebleOrder: SelectedUpdateOrder)
            };
            FrameSource = view;
            MenuIsVisible = Visibility.Collapsed;
        }

        private bool CanExecuteSelectDeleteItem(object obj) =>
            !(SelectedDeleteOrder is null);

        private void ExecuteSelectDeleteItem(object obj)
        {
            var view = new DeleteOrderView
            {
                DataContext = new DeleteOrderViewModel(deletebleOrder: SelectedDeleteOrder)
            };
            FrameSource = view;
            MenuIsVisible = Visibility.Collapsed;
        }

        private void ShowViewPage(object obj)
        {
            var view = new ReadOrderView()
            {
                DataContext = new ReadOrderViewModel(activeUser: _activeUser)
            };
            FrameSource = view;
            MenuIsVisible = Visibility.Collapsed;
        }

        private void ShowCreatePage(object obj)
        {
            var view = new CreateOrderView
            {
                DataContext = new CreateOrderViewModel(activeUser: _activeUser)
            };
            FrameSource = view;
            MenuIsVisible = Visibility.Collapsed;
        }

        private void ShowUpdatePage(object obj)
        {
            var order = Orders.FirstOrDefault();

            if (order != null)
            {
                var view = new UpdateOrderView
                {
                    DataContext = new UpdateOrderViewModel(updatebleOrder: order)
                };
                FrameSource = view;
                MenuIsVisible = Visibility.Collapsed;
            }
            else
                MessageBox.Show(messageBoxText: "Отсутствуют заказы для изменения", caption: "Ошибка",
                    button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private void ShowDeletePage(object obj)
        {
            var order = Orders.FirstOrDefault();

            if (order != null)
            {
                var view = new DeleteOrderView
                {
                    DataContext = new DeleteOrderViewModel(deletebleOrder: order)
                };
                FrameSource = view;
                MenuIsVisible = Visibility.Collapsed;
            }
            else
                MessageBox.Show(messageBoxText: "Отсутствуют заказы для удаления", caption: "Ошибка",
                    button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private void ShowSettingsPage(object obj)
        {
            var view = new SettingsView
            {
                DataContext = new SettingsViewModel(activeUser: _activeUser)
            };
            FrameSource = view;
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
            LogoutCommand = new RelayCommand(Logout);
            ShowViewPageCommand = new RelayCommand(ShowViewPage);
            ShowUpdatePageCommand = new RelayCommand(ShowUpdatePage);
            ShowDeletePageCommand = new RelayCommand(ShowDeletePage);
            ShowCreatePageCommand = new RelayCommand(ShowCreatePage);
            ShowSettingsPageCommand = new RelayCommand(ShowSettingsPage);
            ShowMenuPageCommand = new RelayCommand(ShowMenuPage);
            UpdateDataCollectionCommand = new RelayCommand(UpdateDataCollection);
            CloseApplicationCommand = new RelayCommand(CloseApplication);
        }

        private void InitializeData()
        {
            Orders = _activeUser.Role == UserRole.Admin
                ? _orderRepository.Get().ToList()
                : _orderRepository.Get(order =>
                order.UserId == _activeUser.Id)
                .ToList();
        }

        private void SetViewCondition() =>

#if DEBUG
            _viewTitle = $"{_activeUser.Login} DEBUG MODE";

#else
            _viewTitle = _activeUser.Login;
#endif

        #endregion
    }
}
