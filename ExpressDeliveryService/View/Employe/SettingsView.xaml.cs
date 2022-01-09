using ExpressDeliveryService.ViewModel.Employe;
using System.Windows.Controls;

namespace ExpressDeliveryService.View.Employe
{
    public partial class SettingsView : UserControl
    {
        internal SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
