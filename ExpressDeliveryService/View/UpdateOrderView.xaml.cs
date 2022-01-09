using ExpressDeliveryService.ViewModel;
using System.Windows.Controls;

namespace ExpressDeliveryService.View
{
    public partial class UpdateOrderView : UserControl
    {
        internal UpdateOrderView(UpdateOrderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
