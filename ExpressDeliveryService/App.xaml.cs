using ExpressDeliveryService.View;
using ExpressDeliveryService.View.Popup;
using ExpressDeliveryService.ViewModel;
using ExpressDeliveryService.ViewModel.Popup;
using MVVM.Windows;
using System.Windows;

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
