using ExpressDeliveryService.Services;
using ExpressDeliveryService.View;
using ExpressDeliveryService.ViewModel;
using System.Windows;
using ExpressDeliveryService.View.Popup;
using ExpressDeliveryService.ViewModel.Popup;

namespace ExpressDeliveryService
{
    public partial class App : Application
    {
        public DisplayWindowService DisplayWindow { get; private set; } = new DisplayWindowService();

        public App()
        {
            RegisterWindows();
        }

        private void RegisterWindows()
        {
            DisplayWindow.RegisterWindow<LoginViewModel, LoginView>();
            DisplayWindow.RegisterWindow<MainViewModel, MainView>();
            DisplayWindow.RegisterWindow<MapPopupViewModel, MapPopupView>();
        }
    }
}
