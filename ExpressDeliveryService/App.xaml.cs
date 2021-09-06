using ExpressDeliveryService.Service;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ExpressDeliveryService
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistryService displayRootRegistry = new DisplayRootRegistryService();
        public LoginViewModel loginViewModel;
        public MainViewModel mainViewModel;

        public App()
        {
            displayRootRegistry.RegisterWindowType<LoginViewModel, LoginView>();
            displayRootRegistry.RegisterWindowType<MainViewModel, MainView>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            loginViewModel = new LoginViewModel();

            await displayRootRegistry.ShowModalPresentation(loginViewModel);

            Shutdown();
        }
    }
}
