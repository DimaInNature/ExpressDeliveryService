using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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

            _currentOrder = _orderRepository
                .Get(order => order.Id == deletebleOrder.Id)
                .First();

            InitializeProperties();
            InitializeCommands();
        }

        private OrderModel _currentOrder;

        #region Properties

        public string FromPlace
        {
            get => _fromPlace;
            set
            {
                _fromPlace = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(FromPlace));
            }
        }

        private string _fromPlace;

        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                _fromDate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }

        private DateTime _fromDate = DateTime.UtcNow;

        public string FromTime
        {
            get => _fromTime;
            set
            {
                _fromTime = Regex.IsMatch(input: value, pattern: "[1-2]?[0-9]:[0-5][0-9]")
                    ? value
                    : "09:00";
                OnPropertyChanged(nameof(FromTime));
            }
        }

        private string _fromTime;

        public string ToPlace
        {
            get => _toPlace;
            set
            {
                _toPlace = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(ToPlace));
            }
        }

        private string _toPlace;

        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged(nameof(ToDate));
            }
        }

        private DateTime _toDate = DateTime.UtcNow;

        public string ToTime
        {
            get => _toTime;
            set
            {
                _toTime = Regex.IsMatch(input: value, pattern: "[1-2]?[0-9]:[0-5][0-9]")
                  ? value
                  : "09:00";
                OnPropertyChanged(nameof(ToTime));
            }
        }

        private string _toTime;

        public string BoxWidth
        {
            get => _boxWidth;
            set
            {
                try
                {
                    var input = Convert.ToDouble(value);

                    _boxWidth = input > 9 && input < 200
                        ? value
                        : "10";
                }
                catch
                {
                    _boxWidth = string.Empty;
                }
                OnPropertyChanged(nameof(BoxWidth));
            }
        }

        private string _boxWidth;

        public string BoxHeight
        {
            get => _boxHeight;
            set
            {
                try
                {
                    var input = Convert.ToDouble(value);

                    _boxHeight = input > 9 && input < 200
                        ? value
                        : "10";
                }
                catch
                {
                    _boxHeight = string.Empty;
                }
                OnPropertyChanged(nameof(BoxHeight));
            }
        }

        private string _boxHeight;

        public string BoxLenght
        {
            get => _boxLenght;
            set
            {
                try
                {
                    var input = Convert.ToDouble(value);

                    _boxLenght = input > 9 && input < 200
                        ? value
                        : "10";
                }
                catch
                {
                    _boxLenght = string.Empty;
                }
                OnPropertyChanged(nameof(BoxLenght));
            }
        }

        private string _boxLenght;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string _productName;

        public string ProductCost
        {
            get => _productCost;
            set
            {
                try
                {
                    var input = Convert.ToDouble(value);

                    _productCost = input > 0
                        ? value
                        : "100";
                }
                catch
                {
                    _productCost = string.Empty;
                }
                OnPropertyChanged(nameof(ProductCost));
            }
        }

        private string _productCost;

        public string ProductWeight
        {
            get => _productWeight;
            set
            {
                try
                {
                    var input = Convert.ToDouble(value);

                    _productWeight = input > 0 && input < 10000
                        ? value
                        : "100";
                }
                catch
                {
                    _productWeight = string.Empty;
                }
                OnPropertyChanged(nameof(ProductWeight));
            }
        }

        private string _productWeight;

        public bool AvailabilityOfInsurancePurchased
        {
            get => _availabilityOfInsurancePurchased;
            set
            {
                _availabilityOfInsurancePurchased = value;
                OnPropertyChanged(nameof(AvailabilityOfInsurancePurchased));
            }
        }

        private bool _availabilityOfInsurancePurchased;

        public bool ComplianceTemperatureRegimePurchased
        {
            get => _complianceTemperatureRegimePurchased;
            set
            {
                _complianceTemperatureRegimePurchased = value;
                OnPropertyChanged(nameof(ComplianceTemperatureRegimePurchased));
            }
        }

        private bool _complianceTemperatureRegimePurchased;

        public bool PackagingPurchased
        {
            get => _packagingPurchased;
            set
            {
                _packagingPurchased = value;
                OnPropertyChanged(nameof(PackagingPurchased));
            }
        }

        private bool _packagingPurchased;

        public double TotalCost
        {
            get => _totalCost;
            set
            {
                _totalCost = value > 0
                    ? value
                    : 0;
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        private double _totalCost;

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
            _orderRepository.Remove(_currentOrder);

            MessageBox.Show(messageBoxText: "Заказ был удалён", caption: "Успех",
                button: MessageBoxButton.OK, icon: MessageBoxImage.Information);

            _currentOrder = null;
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

        private void InitializeProperties()
        {
            FromPlace = _currentOrder.FromPlace;
            FromDate = (DateTime)_currentOrder.FromDate;
            FromTime = _currentOrder.FromTime;
            ToPlace = _currentOrder.ToPlace;
            ToDate = (DateTime)_currentOrder.ToDate;
            ToTime = _currentOrder.ToTime;
            BoxWidth = _currentOrder.Box.Width.ToString();
            BoxHeight = _currentOrder.Box.Height.ToString();
            BoxLenght = _currentOrder.Box.Length.ToString();
            ProductName = _currentOrder.Product.Name;
            ProductCost = _currentOrder.Product.Cost.ToString();
            ProductWeight = _currentOrder.Product.Weight.ToString();
            AvailabilityOfInsurancePurchased = _currentOrder.AvailabilityOfInsurancePurchased == "True";
            ComplianceTemperatureRegimePurchased = _currentOrder.ComplianceTemperatureRegimePurchased == "True";
            PackagingPurchased = _currentOrder.PackagingPurchased == "True";
            TotalCost = _currentOrder.TotalCost;
        }

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
