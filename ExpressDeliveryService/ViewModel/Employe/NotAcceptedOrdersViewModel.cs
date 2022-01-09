using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using Models.Enums;
using MVVM.Command;
using MVVM.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Employe
{
    internal class NotAcceptedOrdersViewModel : BaseViewModel
    {
        internal NotAcceptedOrdersViewModel() { }

        internal NotAcceptedOrdersViewModel(EmployeModel activeUser)
        {
            _currentUser = activeUser;

            InitializeCommand();
            InitializeRepositories();
            InitializeData();
        }

        private readonly EmployeModel _currentUser;

        #region Properties

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

        public OrderModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        private OrderModel _selectedOrder;

        #endregion

        #region Commands

        public ICommand AcceptOrderCommand { get; private set; }

        public ICommand ShowMapCommand { get; private set; }

        #endregion

        #region Repositories

        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #region Command Methods

        private async void ExecuteShowMap(object obj) =>
            await (Application.Current as App).DisplayWindow
            .ShowDialog(viewModel: new MapPopupViewModel(streetSource: obj as string));

        private bool CanExecuteShowMap(object obj) =>
            !string.IsNullOrWhiteSpace(obj as string);

        private void ExecuteAcceptOrder(object obj)
        {
            SelectedOrder.Status = OrderStatus.Accepted;
            SelectedOrder.PerformerId = _currentUser.Id;

            _orderRepository.Update(SelectedOrder);

            SelectedOrder = null;

            InitializeData();
        }

        private bool CanExecuteAcceptOrder(object obj) =>
            SelectedOrder != null;

        #endregion

        #region Other Methods

        private void InitializeCommand()
        {
            AcceptOrderCommand = new RelayCommand(executeAction: ExecuteAcceptOrder,
                canExecuteFunc: CanExecuteAcceptOrder);
            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
               canExecuteFunc: CanExecuteShowMap);
        }

        private void InitializeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
        }

        private void InitializeData()
        {
            Orders = _orderRepository
                .Get(order => order.Status == OrderStatus.NotAccepted
                && order.PerformerId == null)
                .ToList();
        }

        #endregion
    }
}
