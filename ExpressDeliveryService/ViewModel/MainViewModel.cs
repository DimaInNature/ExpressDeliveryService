﻿using System.Windows;
using System.Windows.Input;
using ExpressDeliveryService.Service.Command;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel.Base;

namespace ExpressDeliveryService.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
        }

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


        #endregion

    }
}
