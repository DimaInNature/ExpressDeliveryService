using Common;
using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using Models.Enums;
using MVVM.Command;
using MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Employe
{
    public class ScrollerOrdersViewModel : BaseViewModel
    {
        internal ScrollerOrdersViewModel() { }

        internal ScrollerOrdersViewModel(EmployeModel activeUser)
        {
            _activeUser = activeUser;

            InitializeRepositories();
            InitializeQueue();
            InitializeCommand();
            DequeueOrder();
        }

        private readonly EmployeModel _activeUser;

        #region Properties

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

        #region Data

        private Queue<int> _orderIdentitificators;

        #endregion

        #endregion

        #region Commands

        public ICommand ShowNextOrderCommand { get; private set; }

        public ICommand AcceptOrderCommand { get; private set; }

        public ICommand ShowMapCommand { get; private set; }

        #endregion

        #region Repositories

        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #region Command Methods

        private bool CanExecuteShowNextOrder(object obj) =>
            _orderIdentitificators.Count > 0;

        private void ExecuteShowNextOrder(object obj)
        {
            SelectedOrder = null;

            DequeueOrder();
        }

        private async void ExecuteShowMap(object obj) =>
            await (Application.Current as App).DisplayWindow
            .ShowDialog(viewModel: new MapPopupViewModel(streetSource: obj as string));

        private bool CanExecuteShowMap(object obj) =>
            !string.IsNullOrWhiteSpace(obj as string);

        private void ExecuteAcceptOrder(object obj)
        {
            SelectedOrder.Status = OrderStatus.Accepted;
            SelectedOrder.PerformerId = _activeUser.Id;

            _orderRepository.Update(SelectedOrder);

            SelectedOrder = null;

            DequeueOrder();
        }

        private bool CanExecuteAcceptOrder(object obj) =>
            SelectedOrder != null;

        #endregion

        #region Other Methods

        private void DequeueOrder()
        {
            /* An employee cannot have more than five orders. Therefore,
            we check every time that there are no more than five of them. => */

            var ordersCount = _orderRepository
                .Get(order => order.PerformerId == _activeUser.Id &&
                order.Status == OrderStatus.Accepted).Count();

            if (ordersCount <= 5)
            {
                if (_orderIdentitificators.Count > 0)
                {
                    SelectedOrder = _orderRepository
                        .FindById(id: _orderIdentitificators.Dequeue());

                    /* We calculate the distance between the point where the employee
                    is located and the place where employe need to pick up the order. => */

                    double distance = GoogleMapHelper.GetDistanceBetweenTwoKeywords(fromKey:
                        _activeUser.FavouriteLocation, toKey: SelectedOrder.FromPlace);

                    SelectedOrder.Distance = Math.Round(value: SelectedOrder.Distance + distance,
                        digits: 0, mode: MidpointRounding.ToEven);

                    OnPropertyChanged(nameof(SelectedOrder));
                }
                else
                    MessageBox.Show(messageBoxText: "Заказы закончились", caption: "Информация",
                        button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
            }
            else
                MessageBox.Show(messageBoxText: "Превышен лимит взятых заказов", caption: "Информация",
                        button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private void InitializeCommand()
        {
            ShowNextOrderCommand = new RelayCommand(executeAction: ExecuteShowNextOrder,
                canExecuteFunc: CanExecuteShowNextOrder);

            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
                canExecuteFunc: CanExecuteShowMap);

            AcceptOrderCommand = new RelayCommand(executeAction: ExecuteAcceptOrder,
                canExecuteFunc: CanExecuteAcceptOrder);
        }

        private void InitializeQueue() =>
            _orderIdentitificators = new Queue<int>(collection:
                _orderRepository.Get(order => order.Status == OrderStatus.NotAccepted)
                .OrderBy(x =>
                {
                    /* Sort by the distance between the three points.
                    This is necessary so that the closest order is displayed in the scroller first. */

                    double distance = GoogleMapHelper.GetDistanceBetweenTwoKeywords(fromKey:
                        _activeUser.FavouriteLocation, toKey: x.FromPlace);

                    return x.Distance + distance;
                })
                .Select(x => x.Id));

        private void InitializeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
        }

        #endregion
    }
}
