﻿using ExpressDeliveryService.Services;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel;
using System.Windows;

namespace ExpressDeliveryService
{
    public partial class App : Application
    {
        public DisplayWindowService DisplayWindow { get; private set; } = new DisplayWindowService();

        public App()
        {
            // Регистрация связей всех ViewModel и их View

            DisplayWindow.RegisterWindow<LoginViewModel, LoginView>();
            DisplayWindow.RegisterWindow<MainViewModel, MainView>();
        }
    }
}
