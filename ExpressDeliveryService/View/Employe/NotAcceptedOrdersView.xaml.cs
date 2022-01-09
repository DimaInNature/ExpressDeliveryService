using ExpressDeliveryService.ViewModel.Employe;
using System.Windows.Controls;

namespace ExpressDeliveryService.View.Employe
{
    public partial class NotAcceptedOrdersView : UserControl
    {
        internal NotAcceptedOrdersView(NotAcceptedOrdersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
