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
    internal class AcceptedOrdersViewModel : BaseViewModel
    {
        internal AcceptedOrdersViewModel() { }

        internal AcceptedOrdersViewModel(EmployeModel activeUser)
        {
            _currentUser = activeUser;

            InitializeRepositories();
            InitializeCommand();
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

        public ICommand FinishOrderCommand { get; private set; }

        public ICommand CancelOrderCommand { get; private set; }

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

        private void ExecuteFinishOrder(object obj)
        {
            SelectedOrder.Status = OrderStatus.Finished;

            _orderRepository.Update(SelectedOrder);

            SelectedOrder = null;

            InitializeData();
        }

        private bool CanExecuteFinishOrder(object obj) =>
            SelectedOrder != null;

        private void ExecuteCancelOrder(object obj)
        {
            SelectedOrder.Status = OrderStatus.NotAccepted;
            SelectedOrder.PerformerId = null;

            _orderRepository.Update(SelectedOrder);

            SelectedOrder = null;

            InitializeData();
        }

        private bool CanExecuteCancelOrder(object obj) =>
            SelectedOrder != null;

        #endregion

        #region Other Methods

        private void InitializeCommand()
        {
            FinishOrderCommand = new RelayCommand(executeAction: ExecuteFinishOrder,
                canExecuteFunc: CanExecuteFinishOrder);

            CancelOrderCommand = new RelayCommand(executeAction: ExecuteCancelOrder,
                canExecuteFunc: CanExecuteCancelOrder);

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
                .Get(order => order.Status == OrderStatus.Accepted &&
                order.PerformerId == _currentUser.Id)
                .ToList();
        }

        #endregion
    }
}
