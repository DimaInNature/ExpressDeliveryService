using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class DeleteOrderViewModel : BaseViewModel
    {
        internal DeleteOrderViewModel() { }

        internal DeleteOrderViewModel(OrderModel deletebleOrder)
        {
            InitializeRepositories();

            CurrentOrder = _orderRepository
                .Get(order => order.Id == deletebleOrder.Id)
                .First();

            InitializeCommands();
        }

        #region Properties

        public OrderModel CurrentOrder
        {
            get => _currentOrder;
            set
            {
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }

        private OrderModel _currentOrder;

        #endregion

        #region Commands

        public ICommand DeleteOrderCommand { get; private set; }

        public ICommand ShowMapCommand { get; private set; }

        #endregion

        #region Repositories

        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #region Command Methods

        private void ExecuteDeleteOrder(object obj)
        {
            _orderRepository.Remove(CurrentOrder);

            CurrentOrder = null;

            MessageBox.Show(messageBoxText: "Заказ был удалён", caption: "Успех",
                button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private bool CanExecuteDeleteOrder(object obj) =>
            !(_currentOrder is null);

        private async void ExecuteShowMap(object obj) =>
            await (Application.Current as App).DisplayWindow
            .ShowDialog(viewModel: new MapPopupViewModel(streetSource: obj as string));

        private bool CanExecuteShowMap(object obj) =>
            !string.IsNullOrWhiteSpace(obj as string);

        #endregion

        #region Other Methods

        private void InitializeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
        }

        private void InitializeCommands()
        {
            DeleteOrderCommand = new RelayCommand(executeAction: ExecuteDeleteOrder,
                canExecuteFunc: CanExecuteDeleteOrder);

            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
                canExecuteFunc: CanExecuteShowMap);
        }

        #endregion
    }
}
