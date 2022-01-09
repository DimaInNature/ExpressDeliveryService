using ExpressDeliveryService.ViewModel.Employe;
using System.Windows.Controls;

namespace ExpressDeliveryService.View.Employe
{
    public partial class ScrollerOrdersView : UserControl
    {
        internal ScrollerOrdersView(ScrollerOrdersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
