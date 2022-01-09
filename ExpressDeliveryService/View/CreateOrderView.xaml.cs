using ExpressDeliveryService.ViewModel;
using System.Windows.Controls;

namespace ExpressDeliveryService.View
{
    public partial class CreateOrderView : UserControl
    {
        internal CreateOrderView(CreateOrderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
