using ExpressDeliveryService.ViewModel.Employe;
using System.Windows.Controls;

namespace ExpressDeliveryService.View.Employe
{
    public partial class AcceptedOrdersView : UserControl
    {
        internal AcceptedOrdersView(AcceptedOrdersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
