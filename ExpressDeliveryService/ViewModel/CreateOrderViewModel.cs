using Common;
using Data.Repositories;
using Data.Repositories.Abstract;
using ExpressDeliveryService.ViewModel.Popup;
using Models;
using MVVM.Command;
using MVVM.ViewModel;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class CreateOrderViewModel : BaseViewModel
    {
        internal CreateOrderViewModel() { }

        internal CreateOrderViewModel(UserModel activeUser)
        {
            _currentUser = activeUser;

            InititalizeRepositories();
            InitializeCommands();
        }

        private readonly UserModel _currentUser;

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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
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
                CalculateTotalCost();
            }
        }

        private bool _packagingPurchased;

        public double TotalCost { get; set; }

        #endregion

        #region Repositories

        private IGenericRepository<BoxModel> _boxRepository;
        private IGenericRepository<ProductModel> _productRepository;
        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #region Commands

        public ICommand CreateOrderCommand { get; private set; }

        public ICommand ShowMapCommand { get; private set; }

        #endregion

        #region Command Logic

        private async void ExecuteShowMap(object obj) =>
            await (Application.Current as App).DisplayWindow
            .ShowDialog(viewModel: new MapPopupViewModel(streetSource: obj as string));

        private bool CanExecuteShowMap(object obj) =>
            !string.IsNullOrWhiteSpace(obj as string);

        private void ExecuteCreateOrder(object obj)
        {
            var box = BoxModel.CreateBuilder()
                .SetHeight(Convert.ToDouble(BoxHeight))
                .SetLength(Convert.ToDouble(BoxLenght))
                .SetWidth(Convert.ToDouble(BoxWidth));

            _boxRepository.Create(box);

            var product = ProductModel.CreateBuilder()
                 .SetName(ProductName)
                 .SetCost(Convert.ToDouble(ProductCost))
                 .SetWeight(Convert.ToInt32(ProductWeight));

            _productRepository.Create(product);

            double distance = GoogleMapHelper.GetDistanceBetweenTwoKeywords(fromKey:
                FromPlace, toKey: ToPlace);

            var order = OrderModel.CreateBuilder()
                .SetBox(box)
                .SetProduct(product)
                .SetUser(_currentUser)
                .SetFromPlace(FromPlace)
                .SetFromDate(FromDate)
                .SetFromTime(FromTime)
                .SetToPlace(ToPlace)
                .SetToDate(ToDate)
                .SetToTime(ToTime)
                .SetTotalCost(TotalCost)
                .SetPackagingPurchased(PackagingPurchased)
                .SetAvailabilityOfInsurancePurchased(AvailabilityOfInsurancePurchased)
                .SetComplianceTemperatureRegimePurchased(ComplianceTemperatureRegimePurchased)
                .SetDistance(distance);

            _orderRepository.Create(order);

            MessageBox.Show(messageBoxText: "Заказ был создан", caption: "Успех",
                button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private bool CanExecuteCreateOrder(object obj) =>
           !(_currentUser is null) && TotalCost >= 0 &&
            StringHelper.StrIsNotNullOrWhiteSpace(FromPlace, FromTime, ToPlace,
                ToTime, BoxWidth, BoxHeight, BoxLenght, ProductName, ProductCost, ProductWeight);

        #endregion

        #region Other Logic

        private void CalculateTotalCost()
        {
            TotalCost = 0;

            BoxModel box;
            ProductModel product;
            OrderModel order;

            if (StringHelper.StrIsNotNullOrWhiteSpace(BoxHeight, BoxLenght, BoxWidth))
                box = BoxModel.CreateBuilder()
                    .SetWidth(Convert.ToDouble(BoxWidth))
                    .SetHeight(Convert.ToDouble(BoxHeight))
                    .SetLength(Convert.ToDouble(BoxLenght));
            else
                box = null;

            if (StringHelper.StrIsNotNullOrWhiteSpace(ProductCost, ProductWeight))
                product = ProductModel.CreateBuilder()
                    .SetWeight(Convert.ToInt32(ProductWeight))
                    .SetCost(Convert.ToDouble(ProductCost));
            else
                product = null;

            if (StringHelper.StrIsNotNullOrWhiteSpace(FromPlace, ToPlace))
                order = OrderModel.CreateBuilder()
                    .SetAvailabilityOfInsurancePurchased(AvailabilityOfInsurancePurchased)
                    .SetComplianceTemperatureRegimePurchased(ComplianceTemperatureRegimePurchased)
                    .SetPackagingPurchased(PackagingPurchased)
                    .SetFromPlace(FromPlace)
                    .SetToPlace(ToPlace);
            else
                order = null;

            if (box != null && product != null && order != null)
            {
                TotalCostCalculator.Calculate(
                boxData: box,
                productData: product,
                orderData: order,
                result: out double totalCost);

                TotalCost = totalCost;
            }
                
            OnPropertyChanged(nameof(TotalCost));
        }

        private void InitializeCommands()
        {
            CreateOrderCommand = new RelayCommand(executeAction: ExecuteCreateOrder,
                canExecuteFunc: CanExecuteCreateOrder);

            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
                canExecuteFunc: CanExecuteShowMap);
        }

        private void InititalizeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
            _productRepository = new EFGenericRepository<ProductModel>();
            _boxRepository = new EFGenericRepository<BoxModel>();
        }

        #endregion
    }
}
