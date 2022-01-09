using ExpressDeliveryService.ViewModel;
using System.Windows.Controls;

namespace ExpressDeliveryService.View
{
    public partial class DeleteOrderView : UserControl
    {
        internal DeleteOrderView(DeleteOrderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
