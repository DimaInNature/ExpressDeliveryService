using Common;
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
    internal sealed class UpdateOrderViewModel : BaseViewModel
    {
        internal UpdateOrderViewModel() { }

        internal UpdateOrderViewModel(OrderModel updatebleOrder)
        {
            InitializeRepositories();

            _currentOrder = _orderRepository
                .Get(order => order.Id == updatebleOrder.Id)
                .First();

            InitializeProperties();
            InitializeCommands();
        }

        private readonly OrderModel _currentOrder;

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

        #region Commands

        public ICommand UpdateOrderCommand { get; private set; }

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

        private void ExecuteUpdateOrder(object obj)
        {
            var box = BoxModel.CreateBuilder()
                .SetId(_currentOrder.BoxId)
                .SetLength(_currentOrder.Box.Length)
                .SetHeight(_currentOrder.Box.Height);

            var product = ProductModel.CreateBuilder()
                .SetId(_currentOrder.ProductId)
                .SetName(_currentOrder.Product.Name)
                .SetWeight(_currentOrder.Product.Weight)
                .SetCost(_currentOrder.Product.Cost);

            double distance = Math.Round(value:
                GoogleMapHelper.GetDistanceBetweenTwoKeywords(fromKey: FromPlace, toKey: ToPlace),
                digits: 2, mode: MidpointRounding.ToEven);

            var order = OrderModel.CreateBuilder()
                .SetId(_currentOrder.Id)
                .SetFromPlace(FromPlace)
                .SetFromDate(FromDate)
                .SetFromTime(FromTime)
                .SetToPlace(ToPlace)
                .SetToDate(ToDate)
                .SetToTime(ToTime)
                .SetAvailabilityOfInsurancePurchased(AvailabilityOfInsurancePurchased)
                .SetComplianceTemperatureRegimePurchased(ComplianceTemperatureRegimePurchased)
                .SetPackagingPurchased(PackagingPurchased)
                .SetTotalCost(TotalCost)
                .SetUser(_currentOrder.UserId)
                .SetProduct(product)
                .SetBox(box)
                .SetDistance(distance);

            _orderRepository.Update(order);

            MessageBox.Show(messageBoxText: "Заказ был изменён", caption: "Успех",
                button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
        }

        private bool CanExecuteUpdateOrder(object obj) =>
           !(_currentOrder is null) && TotalCost >= 0 &&
            StringHelper.StrIsNotNullOrWhiteSpace(FromPlace, FromTime,
                ToPlace, ToTime, BoxWidth, BoxHeight, BoxLenght, ProductName,
                ProductCost, ProductWeight);

        #endregion

        #region Other Methods

        private void CalculateTotalCost()
        {
            TotalCost = 0;

            if (StringHelper.StrIsNotNullOrWhiteSpace(BoxHeight, BoxLenght, BoxWidth))
                TotalCost += Convert.ToDouble(BoxHeight) * Convert.ToDouble(BoxLenght)
                    * Convert.ToDouble(BoxWidth) / 30;

            if (!string.IsNullOrWhiteSpace(ProductCost) && Convert.ToDouble(ProductCost) > 0)
                TotalCost += Convert.ToDouble(ProductCost) / 5;

            if (!string.IsNullOrWhiteSpace(ProductWeight) && Convert.ToDouble(ProductWeight) > 0)
                TotalCost += Convert.ToDouble(ProductWeight) / 10;

            if (AvailabilityOfInsurancePurchased && !string.IsNullOrWhiteSpace(ProductCost))
                TotalCost += Convert.ToDouble(ProductCost) / 10;

            if (PackagingPurchased && StringHelper.StrIsNotNullOrWhiteSpace(BoxHeight, BoxLenght, BoxWidth))
                TotalCost += 150;

            if (ComplianceTemperatureRegimePurchased)
                TotalCost += TotalCost / 100 * 20;

            if (!string.IsNullOrWhiteSpace(FromPlace) && !string.IsNullOrWhiteSpace(ToPlace))
            {
                double distance = GoogleMapHelper.GetDistanceBetweenTwoKeywords(fromKey:
                    FromPlace, toKey: ToPlace);

                TotalCost += distance / 1000 / 100 * 20;
            }

            TotalCost = Math.Round(value: TotalCost,
                digits: 2, mode: MidpointRounding.ToEven);

            OnPropertyChanged(nameof(TotalCost));
        }

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
            UpdateOrderCommand = new RelayCommand(executeAction: ExecuteUpdateOrder,
                canExecuteFunc: CanExecuteUpdateOrder);

            ShowMapCommand = new RelayCommand(executeAction: ExecuteShowMap,
                canExecuteFunc: CanExecuteShowMap);
        }

        #endregion
    }
}
