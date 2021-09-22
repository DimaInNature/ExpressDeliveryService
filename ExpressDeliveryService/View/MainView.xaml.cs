using System.Windows;
using ExpressDeliveryService.ViewModel;

namespace ExpressDeliveryService.View
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private MainViewModel ViewModel
        {
            get { return (MainViewModel)DataContext; }
        }

    }
}
