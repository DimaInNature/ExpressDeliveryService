using System;
using ExpressDeliveryService.Model;
using ExpressDeliveryService.Services.Command;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ExpressDeliveryService.Services;

namespace ExpressDeliveryService.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        DataManager db = new DataManager("ExpressDeliveryDB");

        public MainViewModel()
        {
            _orders = db.GetAll<Order>("Orders");

            CreateButtonClickCommand = new DelegateCommandService(CreateButtonClick);
            EditButtonClickCommand = new DelegateCommandService(EditButtonClick);
            EditMenuItemClickCommand = new DelegateCommandService(EditMenuItemClick);
            DeleteButtonClickCommand = new DelegateCommandService(DeleteButtonClick);
            DeleteMenuItemClickCommand = new DelegateCommandService(DeleteMenuItemClick);
        }

        #region Properties

        #region Data Collections

        public List<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }

        private List<Order> _orders;

        public Order SelectedViewOrder
        {
            get => _selectedViewOrder;
            set
            {
                _selectedViewOrder = value;
                OnPropertyChanged("SelectedViewOrder");
            }
        }

        private Order _selectedViewOrder;

        public Order SelectedEditOrder
        {
            get => _selectedEditOrder;
            set
            {
                _selectedEditOrder = value;
                OnPropertyChanged("SelectedEditOrder");
            }
        }

        private Order _selectedEditOrder;

        public Order SelectedDeleteOrder
        {
            get => _selectedDeleteOrder;
            set
            {
                _selectedDeleteOrder = value;
                OnPropertyChanged("SelectedDeleteOrder");
            }
        }

        private Order _selectedDeleteOrder;

        #endregion

        #region Create

        #region From

        public string CreateFromPlace
        {
            get => _createFromPlace;
            set
            {
                if (value != String.Empty)
                {
                    _createFromPlace = value;
                    OnPropertyChanged("CreateFromPlace");
                }
                else
                {
                    _createFromPlace = String.Empty;
                    OnPropertyChanged("CreateFromPlace");
                }
            }
        }

        private string _createFromPlace;

        public DateTime CreateFromDate
        {
            get => _createFromDate;
            set
            { 
                _createFromDate = value; 
                OnPropertyChanged("CreateFromDate");
            }
        }

        private DateTime _createFromDate = new DateTime(DateTime.UtcNow.Year,
            DateTime.UtcNow.Month,01, 0, 0, 0,DateTimeKind.Utc);

        public string CreateFromTime
        {
            get => _createFromTime;
            set
            {
                if ((value != String.Empty) && (value.Length == 5))
                {
                    _createFromTime = value;
                    OnPropertyChanged("CreateFromTime");
                }
                else
                {
                    _createFromTime = "00:00";
                    OnPropertyChanged("CreateFromTime");
                }
            }
        }

        private string _createFromTime;

        #endregion

        #region To

        public string CreateToPlace
        {
            get => _createToPlace;
            set
            {
                if (value != String.Empty)
                {
                    _createToPlace = value;
                    OnPropertyChanged("CreateToPlace");
                }
                else
                {
                    _createToPlace = String.Empty;
                    OnPropertyChanged("CreateToPlace");
                }
            }
        }

        private string _createToPlace;

        public DateTime CreateToDate
        {
            get => _createToDate;
            set
            {
                _createToDate = value;
                OnPropertyChanged("CreateToDate");
            }
        }

        private DateTime _createToDate = new DateTime(DateTime.UtcNow.Year,
            DateTime.UtcNow.Month + 1, 01, 0, 0, 0, DateTimeKind.Utc);

        public string CreateToTime
        {
            get => _createToTime;
            set
            {
                if ((value != String.Empty) && (value.Length == 5))
                {
                    _createToTime = value;
                    OnPropertyChanged("CreateToTime");
                }
                else
                {
                    _createToTime = "00:00";
                    OnPropertyChanged("CreateToTime");
                }
            }
        }

        private string _createToTime;

        #endregion

        #region Box

        public string CreateBoxWidth
        {
            get => _createBoxWidth;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && (Convert.ToDouble(value) < 200))
                    {
                        _createBoxWidth = value;
                        OnPropertyChanged("CreateBoxWidth");
                    }
                    else
                    {
                        _createBoxWidth = "0";
                        OnPropertyChanged("CreateBoxWidth");
                    }
                }
                catch
                {
                    _createBoxWidth = String.Empty;
                    OnPropertyChanged("CreateBoxWidth");
                }
            }
        }

        private string _createBoxWidth;

        public string CreateBoxHeight
        {
            get => _createBoxHeight;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _createBoxHeight = value;
                        OnPropertyChanged("CreateBoxHeight");
                    }
                    else
                    {
                        _createBoxHeight = "0";
                        OnPropertyChanged("CreateBoxHeight");
                    }
                }
                catch
                {
                    _createBoxHeight = String.Empty;
                    OnPropertyChanged("CreateBoxHeight");
                }

            }
        }

        private string _createBoxHeight;

        public string CreateBoxLenght
        {
            get => _createBoxLenght;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _createBoxLenght = value;
                        OnPropertyChanged("CreateBoxLenght");
                    }
                    else
                    {
                        _createBoxLenght = "0";
                        OnPropertyChanged("CreateBoxLenght");
                    }
                }
                catch
                {
                    _createBoxLenght = String.Empty;
                    OnPropertyChanged("CreateBoxLenght");
                }
            }
        }

        private string _createBoxLenght;

        #endregion

        #region Product

        public string CreateProductName
        {
            get => _createProductName;
            set
            {
                if (value != string.Empty)
                {
                    _createProductName = value;
                    OnPropertyChanged("CreateProductName");
                }
                else
                {
                    _createProductName = String.Empty;
                    OnPropertyChanged("CreateProductName");
                }
            }
        }

        private string _createProductName;

        public string CreateProductCost
        {
            get => _createProductCost;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0)
                    {
                        _createProductCost = value;
                        OnPropertyChanged("CreateProductCost");
                    }
                    else
                    {
                        _createProductCost = "0";
                        OnPropertyChanged("CreateProductCost");
                    }
                }
                catch
                {
                    _createProductCost = String.Empty;
                    OnPropertyChanged("CreateProductCost");
                }
            }
        }

        private string _createProductCost;

        public string CreateProductWeight
        {
            get => _createProductWeight;
            set
            {
                try
                {
                    if (Convert.ToInt32(value) > 0 && Convert.ToInt32(value) < 10000)
                    {
                        _createProductWeight = value;
                        OnPropertyChanged("CreateProductWeight");
                    }
                    else
                    {
                        _createProductWeight = "0";
                        OnPropertyChanged("CreateProductWeight");
                    }
                }
                catch
                {
                    _createProductWeight = String.Empty;
                    OnPropertyChanged("CreateProductWeight");
                }
            }
        }

        private string _createProductWeight;

        #endregion

        #region Услуги

        public bool CreateAvailabilityOfInsurancePurchased
        {
            get => _createAvailabilityOfInsurancePurchased;
            set
            {
                _createAvailabilityOfInsurancePurchased = value;
                OnPropertyChanged("CreateAvailabilityOfInsurancePurchased");
            }
        }

        private bool _createAvailabilityOfInsurancePurchased;

        public bool CreateComplianceTemperatureRegimePurchased
        {
            get => _createComplianceTemperatureRegimePurchased;
            set
            {
                _createComplianceTemperatureRegimePurchased = value;
                OnPropertyChanged("CreateComplianceTemperatureRegimePurchased");
            }
        }

        private bool _createComplianceTemperatureRegimePurchased;

        public bool CreatePackagingPurchased
        {
            get => _createPackagingPurchased;
            set
            {
                _createPackagingPurchased = value;
                OnPropertyChanged("CreatePackagingPurchased");
            }
        }

        private bool _createPackagingPurchased;

        #endregion

        public double CreateTotalCost
        {
            get => _createTotalCost;
            set
            {
                if (Convert.ToDouble(value) > 0)
                {
                    _createTotalCost = Convert.ToDouble(value);
                    OnPropertyChanged("CreateToPlace");
                }
                else
                {
                    _createTotalCost = 0;
                    OnPropertyChanged("CreateToPlace");
                }
            }
        }

        private double _createTotalCost;

        #endregion

        #region Edit

        #region From

        public string EditFromPlace
        {
            get => _editFromPlace;
            set
            {
                if (value != String.Empty)
                {
                    _editFromPlace = value;
                    OnPropertyChanged("EditFromPlace");
                }
                else
                {
                    _editFromPlace = String.Empty;
                    OnPropertyChanged("EditFromPlace");
                }
            }
        }

        private string _editFromPlace;

        public DateTime? EditFromDate
        {
            get => _editFromDate;
            set
            {
                _editFromDate = value;
                OnPropertyChanged("EditFromDate");
            }
        }

        private DateTime? _editFromDate;

        public string EditFromTime
        {
            get => _editFromTime;
            set
            {
                if (value.Length == 5 || value == string.Empty)
                {
                    _editFromTime = value;
                    OnPropertyChanged("EditFromTime");
                }
            }
        }

        private string _editFromTime;

        #endregion

        #region To

        public string EditToPlace
        {
            get => _editToPlace;
            set
            {
                if (value != String.Empty)
                {
                    _editToPlace = value;
                    OnPropertyChanged("EditToPlace");
                }
                else
                {
                    _editToPlace = String.Empty;
                    OnPropertyChanged("EditToPlace");
                }
            }
        }

        private string _editToPlace;

        public DateTime? EditToDate
        {
            get => _editToDate;
            set
            {
                _editToDate = value;
                OnPropertyChanged("EditToDate");
            }
        }

        private DateTime? _editToDate;

        public string EditToTime
        {
            get => _editToTime;
            set
            {
                if (value.Length == 5 || value == string.Empty)
                {
                    _editToTime = value;
                    OnPropertyChanged("EditToTime");
                }
            }
        }

        private string _editToTime;

        #endregion

        #region Box

        public string EditBoxWidth
        {
            get => _editBoxWidth;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && (Convert.ToDouble(value) < 200))
                    {
                        _editBoxWidth = value;
                        OnPropertyChanged("EditBoxWidth");
                    }
                    else
                    {
                        _editBoxWidth = "0";
                        OnPropertyChanged("EditBoxWidth");
                    }
                }
                catch
                {
                    _editBoxWidth = String.Empty;
                    OnPropertyChanged("EditBoxWidth");
                }
            }
        }

        private string _editBoxWidth;

        public string EditBoxHeight
        {
            get => _editBoxHeight;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _editBoxHeight = value;
                        OnPropertyChanged("EditBoxHeight");
                    }
                    else
                    {
                        _editBoxHeight = "0";
                        OnPropertyChanged("EditBoxHeight");
                    }
                }
                catch
                {
                    _editBoxHeight = String.Empty;
                    OnPropertyChanged("EditBoxHeight");
                }

            }
        }

        private string _editBoxHeight;

        public string EditBoxLenght
        {
            get => _editBoxLenght;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _editBoxLenght = value;
                        OnPropertyChanged("EditBoxLenght");
                    }
                    else
                    {
                        _editBoxLenght = "0";
                        OnPropertyChanged("EditBoxLenght");
                    }
                }
                catch
                {
                    _editBoxLenght = String.Empty;
                    OnPropertyChanged("EditBoxLenght");
                }
            }
        }

        private string _editBoxLenght;

        #endregion

        #region Product

        public string EditProductName
        {
            get => _editProductName;
            set
            {
                if (value != string.Empty)
                {
                    _editProductName = value;
                    OnPropertyChanged("EditProductName");
                }
                else
                {
                    _editProductName = String.Empty;
                    OnPropertyChanged("EditProductName");
                }
            }
        }

        private string _editProductName;

        public string EditProductCost
        {
            get => _editProductCost;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0)
                    {
                        _editProductCost = value;
                        OnPropertyChanged("EditProductCost");
                    }
                    else
                    {
                        _editProductCost = "0";
                        OnPropertyChanged("EditProductCost");
                    }
                }
                catch
                {
                    _editProductCost = String.Empty;
                    OnPropertyChanged("EditProductCost");
                }
            }
        }

        private string _editProductCost;

        public string EditProductWeight
        {
            get => _editProductWeight;
            set
            {
                try
                {
                    if (Convert.ToInt32(value) > 0 && Convert.ToInt32(value) < 10000)
                    {
                        _editProductWeight = value;
                        OnPropertyChanged("EditProductWeight");
                    }
                    else
                    {
                        _editProductWeight = "0";
                        OnPropertyChanged("EditProductWeight");
                    }
                }
                catch
                {
                    _editProductWeight = String.Empty;
                    OnPropertyChanged("EditProductWeight");
                }
            }
        }

        private string _editProductWeight;

        #endregion

        #region Услуги

        public bool EditAvailabilityOfInsurancePurchased
        {
            get => _editAvailabilityOfInsurancePurchased;
            set
            {
                _editAvailabilityOfInsurancePurchased = value;
                OnPropertyChanged("EditAvailabilityOfInsurancePurchased");
            }
        }

        private bool _editAvailabilityOfInsurancePurchased;

        public bool EditComplianceTemperatureRegimePurchased
        {
            get => _editComplianceTemperatureRegimePurchased;
            set
            {
                _editComplianceTemperatureRegimePurchased = value;
                OnPropertyChanged("EditComplianceTemperatureRegimePurchased");
            }
        }

        private bool _editComplianceTemperatureRegimePurchased;

        public bool EditPackagingPurchased
        {
            get => _editPackagingPurchased;
            set
            {
                _editPackagingPurchased = value;
                OnPropertyChanged("EditPackagingPurchased");
            }
        }

        private bool _editPackagingPurchased;

        #endregion

        public double EditTotalCost
        {
            get => _editTotalCost;
            set
            {
                if (Convert.ToDouble(value) > 0)
                {
                    _editTotalCost = Convert.ToDouble(value);
                    OnPropertyChanged("EditTotalCost");
                }
                else
                {
                    _editTotalCost = 0;
                    OnPropertyChanged("EditTotalCost");
                }
            }
        }

        private double _editTotalCost;

        #endregion

        #region Delete

        #region From

        public string DeleteFromPlace
        {
            get => _deleteFromPlace;
            set
            {
                if (value != String.Empty)
                {
                    _deleteFromPlace = value;
                    OnPropertyChanged("DeleteFromPlace");
                }
                else
                {
                    _deleteFromPlace = String.Empty;
                    OnPropertyChanged("DeleteFromPlace");
                }
            }
        }

        private string _deleteFromPlace;

        public DateTime? DeleteFromDate
        {
            get => _deleteFromDate;
            set
            {
                _deleteFromDate = value;
                OnPropertyChanged("DeleteFromDate");
            }
        }

        private DateTime? _deleteFromDate;

        public string DeleteFromTime
        {
            get => _deleteFromTime;
            set
            {
                if (value.Length == 5 || value == string.Empty)
                {
                    _deleteFromTime = value;
                    OnPropertyChanged("DeleteFromTime");
                }
            }
        }

        private string _deleteFromTime;

        #endregion

        #region To

        public string DeleteToPlace
        {
            get => _deleteToPlace;
            set
            {
                if (value != String.Empty)
                {
                    _deleteToPlace = value;
                    OnPropertyChanged("DeleteToPlace");
                }
                else
                {
                    _deleteToPlace = String.Empty;
                    OnPropertyChanged("DeleteToPlace");
                }
            }
        }

        private string _deleteToPlace;

        public DateTime? DeleteToDate
        {
            get => _deleteToDate;
            set
            {
                _deleteToDate = value;
                OnPropertyChanged("DeleteToDate");
            }
        }

        private DateTime? _deleteToDate;

        public string DeleteToTime
        {
            get => _deleteToTime;
            set
            {
                if (value.Length == 5 || value == string.Empty)
                {
                    _deleteToTime = value;
                    OnPropertyChanged("DeleteToTime");
                }
            }
        }

        private string _deleteToTime;

        #endregion

        #region Box

        public string DeleteBoxWidth
        {
            get => _deleteBoxWidth;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && (Convert.ToDouble(value) < 200))
                    {
                        _deleteBoxWidth = value;
                        OnPropertyChanged("DeleteBoxWidth");
                    }
                    else
                    {
                        _deleteBoxWidth = "0";
                        OnPropertyChanged("DeleteBoxWidth");
                    }
                }
                catch
                {
                    _deleteBoxWidth = String.Empty;
                    OnPropertyChanged("DeleteBoxWidth");
                }
            }
        }

        private string _deleteBoxWidth;

        public string DeleteBoxHeight
        {
            get => _deleteBoxHeight;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _deleteBoxHeight = value;
                        OnPropertyChanged("DeleteBoxHeight");
                    }
                    else
                    {
                        _deleteBoxHeight = "0";
                        OnPropertyChanged("DeleteBoxHeight");
                    }
                }
                catch
                {
                    _deleteBoxHeight = String.Empty;
                    OnPropertyChanged("DeleteBoxHeight");
                }

            }
        }

        private string _deleteBoxHeight;

        public string DeleteBoxLenght
        {
            get => _deleteBoxLenght;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0 && Convert.ToDouble(value) < 200)
                    {
                        _deleteBoxLenght = value;
                        OnPropertyChanged("DeleteBoxLenght");
                    }
                    else
                    {
                        _deleteBoxLenght = "0";
                        OnPropertyChanged("DeleteBoxLenght");
                    }
                }
                catch
                {
                    _deleteBoxLenght = String.Empty;
                    OnPropertyChanged("DeleteBoxLenght");
                }
            }
        }

        private string _deleteBoxLenght;

        #endregion

        #region Product

        public string DeleteProductName
        {
            get => _deleteProductName;
            set
            {
                if (value != string.Empty)
                {
                    _deleteProductName = value;
                    OnPropertyChanged("DeleteProductName");
                }
                else
                {
                    _deleteProductName = String.Empty;
                    OnPropertyChanged("DeleteProductName");
                }
            }
        }

        private string _deleteProductName;

        public string DeleteProductCost
        {
            get => _deleteProductCost;
            set
            {
                try
                {
                    if (Convert.ToDouble(value) > 0)
                    {
                        _deleteProductCost = value;
                        OnPropertyChanged("DeleteProductCost");
                    }
                    else
                    {
                        _deleteProductCost = "0";
                        OnPropertyChanged("DeleteProductCost");
                    }
                }
                catch
                {
                    _deleteProductCost = String.Empty;
                    OnPropertyChanged("DeleteProductCost");
                }
            }
        }

        private string _deleteProductCost;

        public string DeleteProductWeight
        {
            get => _deleteProductWeight;
            set
            {
                try
                {
                    if (Convert.ToInt32(value) > 0 && Convert.ToInt32(value) < 10000)
                    {
                        _deleteProductWeight = value;
                        OnPropertyChanged("DeleteProductWeight");
                    }
                    else
                    {
                        _deleteProductWeight = "0";
                        OnPropertyChanged("DeleteProductWeight");
                    }
                }
                catch
                {
                    _deleteProductWeight = String.Empty;
                    OnPropertyChanged("DeleteProductWeight");
                }
            }
        }

        private string _deleteProductWeight;

        #endregion

        #region Услуги

        public bool DeleteAvailabilityOfInsurancePurchased
        {
            get => _deleteAvailabilityOfInsurancePurchased;
            set
            {
                _deleteAvailabilityOfInsurancePurchased = value;
                OnPropertyChanged("DeleteAvailabilityOfInsurancePurchased");
            }
        }

        private bool _deleteAvailabilityOfInsurancePurchased;

        public bool DeleteComplianceTemperatureRegimePurchased
        {
            get => _deleteComplianceTemperatureRegimePurchased;
            set
            {
                _deleteComplianceTemperatureRegimePurchased = value;
                OnPropertyChanged("DeleteComplianceTemperatureRegimePurchased");
            }
        }

        private bool _deleteComplianceTemperatureRegimePurchased;

        public bool DeletePackagingPurchased
        {
            get => _deletePackagingPurchased;
            set
            {
                _deletePackagingPurchased = value;
                OnPropertyChanged("DeletePackagingPurchased");
            }
        }

        private bool _deletePackagingPurchased;

        #endregion

        public double DeleteTotalCost
        {
            get => _deleteTotalCost;
            set
            {
                if (Convert.ToDouble(value) > 0)
                {
                    _deleteTotalCost = Convert.ToDouble(value);
                    OnPropertyChanged("DeleteTotalCost");
                }
                else
                {
                    _deleteTotalCost = 0;
                    OnPropertyChanged("DeleteTotalCost");
                }
            }
        }

        private double _deleteTotalCost;

        #endregion

        #endregion

        #region Commands

        #region CreateButtonClickCommand

        public ICommand CreateButtonClickCommand { get; private set; }

        private  void CreateButtonClick(object obj)
        {
            db.Create("Orders", new Order()
            {
                FromPlace = CreateFromPlace,
                FromDate = CreateFromDate,
                FromTime = CreateFromTime,

                ToPlace = CreateToPlace,
                ToDate = CreateToDate,
                ToTime = CreateToTime,

                Product = new Product()
                {
                    Name = CreateProductName,
                    Cost = Convert.ToDouble(CreateProductCost),
                    Weight = Convert.ToInt32(CreateProductWeight)
                },

                Box = new Box()
                {
                    Height = Convert.ToDouble(CreateBoxHeight),
                    Width = Convert.ToDouble(CreateBoxWidth),
                    Lenght = Convert.ToDouble(CreateBoxLenght)
                },

                AvailabilityOfInsurancePurchased = CreateAvailabilityOfInsurancePurchased,
                ComplianceTemperatureRegimePurchased = CreateComplianceTemperatureRegimePurchased,
                PackagingPurchased = CreatePackagingPurchased
            });

            _orders = db.GetAll<Order>("Orders");
            OnPropertyChanged("Orders");
        }

        #endregion

        #region ExitMenuButtonClickCommand

        public ICommand ExitMenuButtonClickCommand { get; private set; } = new DelegateCommandService(ExitMenuButtonClick);

        private static void ExitMenuButtonClick(object obj)
        {
            // Параметр является ссылкой на представление

            var view = obj as MainView;

            var displayRootRegistry = (Application.Current as App).DisplayWindow;

            displayRootRegistry.Show(new LoginViewModel());

            view.Close();
        }

        #endregion

        #region EditButtonClickCommand

        public ICommand EditButtonClickCommand { get; private set; }

        private void EditButtonClick(object obj)
        {
            Order newOrder = new Order()
            {
                Id = SelectedEditOrder.Id,
                FromPlace = EditFromPlace,
                FromDate = EditFromDate,
                FromTime = EditFromTime,

                ToPlace = EditToPlace,
                ToDate = EditToDate,
                ToTime = EditToTime,

                Product = new Product()
                {
                    Name = EditProductName,
                    Cost = Convert.ToDouble(EditProductCost),
                    Weight = Convert.ToInt32(EditProductWeight)
                },

                Box = new Box()
                {
                    Height = Convert.ToDouble(EditBoxHeight),
                    Width = Convert.ToDouble(EditBoxWidth),
                    Lenght = Convert.ToDouble(EditBoxLenght)
                },

                AvailabilityOfInsurancePurchased = EditAvailabilityOfInsurancePurchased,
                ComplianceTemperatureRegimePurchased = EditComplianceTemperatureRegimePurchased,
                PackagingPurchased = EditPackagingPurchased
            };

            db.Edit("Orders", newOrder.Id, newOrder);

            _orders = db.GetAll<Order>("Orders");

            OnPropertyChanged("Orders");

            EditFromPlace = String.Empty;
            EditFromTime = String.Empty;
            EditFromDate = null;

            EditToPlace = String.Empty;
            EditToTime = String.Empty;
            EditToDate = null;

            EditBoxHeight = String.Empty;
            EditBoxLenght = String.Empty;
            EditBoxWidth = String.Empty;

            EditProductCost = String.Empty;
            EditProductName = String.Empty;
            EditProductWeight = String.Empty;

            EditAvailabilityOfInsurancePurchased = false;
            EditComplianceTemperatureRegimePurchased = false;
            EditPackagingPurchased = false;
        }

        #endregion

        #region EditMenuItemClickCommand

        public ICommand EditMenuItemClickCommand { get; private set; }

        private void EditMenuItemClick(object obj)
        {
            EditFromPlace = SelectedEditOrder.FromPlace;
            EditFromTime = SelectedEditOrder.FromTime;
            EditFromDate = SelectedEditOrder.FromDate;

            EditToPlace = SelectedEditOrder.ToPlace;
            EditToTime = SelectedEditOrder.ToTime;
            EditToDate = SelectedEditOrder.ToDate;

            EditBoxHeight = SelectedEditOrder.Box.Height.ToString();
            EditBoxLenght = SelectedEditOrder.Box.Lenght.ToString();
            EditBoxWidth = SelectedEditOrder.Box.Width.ToString();

            EditProductCost = SelectedEditOrder.Product.Cost.ToString();
            EditProductName = SelectedEditOrder.Product.Name;
            EditProductWeight = SelectedEditOrder.Product.Weight.ToString();

            EditAvailabilityOfInsurancePurchased = SelectedEditOrder.AvailabilityOfInsurancePurchased;
            EditComplianceTemperatureRegimePurchased = SelectedEditOrder.ComplianceTemperatureRegimePurchased;
            EditPackagingPurchased = SelectedEditOrder.PackagingPurchased;
        }

        #endregion

        #region DeleteButtonClickCommand

        public ICommand DeleteButtonClickCommand { get; private set; }

        private void DeleteButtonClick(object obj)
        {
            db.Delete<Order>("Orders", SelectedDeleteOrder.Id);
            _orders = db.GetAll<Order>("Orders");

            OnPropertyChanged("Orders");

            DeleteFromPlace = String.Empty;
            DeleteFromTime = String.Empty;
            DeleteFromDate = null;

            DeleteToPlace = String.Empty;
            DeleteToTime = String.Empty;
            DeleteToDate = null;

            DeleteBoxHeight = String.Empty;
            DeleteBoxLenght = String.Empty;
            DeleteBoxWidth = String.Empty;

            DeleteProductCost = String.Empty;
            DeleteProductName = String.Empty;
            DeleteProductWeight = String.Empty;

            DeleteAvailabilityOfInsurancePurchased = false;
            DeleteComplianceTemperatureRegimePurchased = false;
            DeletePackagingPurchased = false;

        }

        #endregion

        #region DeleteMenuItemClickCommand

        public ICommand DeleteMenuItemClickCommand { get; private set; }

        private void DeleteMenuItemClick(object obj)
        {
            DeleteFromPlace = SelectedDeleteOrder.FromPlace;
            DeleteFromTime = SelectedDeleteOrder.FromTime;
            DeleteFromDate = SelectedDeleteOrder.FromDate;

            DeleteToPlace = SelectedDeleteOrder.ToPlace;
            DeleteToTime = SelectedDeleteOrder.ToTime;
            DeleteToDate = SelectedDeleteOrder.ToDate;

            DeleteBoxHeight = SelectedDeleteOrder.Box.Height.ToString();
            DeleteBoxLenght = SelectedDeleteOrder.Box.Lenght.ToString();
            DeleteBoxWidth = SelectedDeleteOrder.Box.Width.ToString();

            DeleteProductCost = SelectedDeleteOrder.Product.Cost.ToString();
            DeleteProductName = SelectedDeleteOrder.Product.Name;
            DeleteProductWeight = SelectedDeleteOrder.Product.Weight.ToString();

            DeleteAvailabilityOfInsurancePurchased = SelectedDeleteOrder.AvailabilityOfInsurancePurchased;
            DeleteComplianceTemperatureRegimePurchased = SelectedDeleteOrder.ComplianceTemperatureRegimePurchased;
            DeletePackagingPurchased = SelectedDeleteOrder.PackagingPurchased;
        }

        #endregion

        #endregion

        #region Methods


        #endregion

    }
}
