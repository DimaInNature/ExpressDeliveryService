using System;
using ExpressDeliveryService.Model;
using ExpressDeliveryService.Service.Command;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            SetExampleData();
        }

        #region Properties

        #region Orders Props

        public static List<Order> Orders { get; set; }

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

        #endregion

        #region Commands

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

        #endregion

        #region Methods

        private void SetExampleData()
        {
            Orders = new List<Order>();
            Orders.Add(new Order
            {
                FromPlace = "FromPlaceTest",
                FromDate = new DateTime(2021, 06, 5),
                FromTime = "00:00",

                ToPlace = "ToPlaceTest",
                ToDate = new DateTime(2022, 06, 5),
                ToTime = "01:00",

                BoxWidth = 20,
                BoxHeight = 25,
                BoxLenght = 30,

                ProductValue = 1110,
                ProductName = "Тестовое имя",
                ProductWeight = 200,

                AvailabilityOfInsuranceService = true,
                ComplianceTemperatureRegimeService = false,
                PackagingService = false,

                TotalCostOfDelivery = 0
            });
        }

        #endregion

    }
}
