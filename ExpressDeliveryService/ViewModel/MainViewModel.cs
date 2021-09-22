using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ExpressDeliveryService.Model;
using ExpressDeliveryService.Service.Command;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Base;

namespace ExpressDeliveryService.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            EditOrders = new List<Order>();
            EditOrders.Add(new Order("Test 1"));
            EditOrders.Add(new Order("Test 2"));
            Ts = new DelegateCommandService((Object) =>
            {
                MessageBox.Show(SelectedEditOrder.ProductName);
            });
        }

        #region Properties

        public static List<Order> EditOrders { get; set; }

        public Order SelectedEditOrder
        {
            get
            {
                return _selectedEditOrder;
            }
            set
            {
                _selectedEditOrder = value;
                OnPropertyChanged("SelectedEditOrder");
            }
        }

        public Order _selectedEditOrder;

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

        #region t

        public ICommand Ts { get; private set; }

        private static void t2(object obj)
        {

            


        }

        #endregion

        #endregion

        #region Methods


        #endregion

    }
}
