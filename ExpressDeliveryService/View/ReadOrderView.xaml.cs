using ExpressDeliveryService.ViewModel;
using System.Windows.Controls;

namespace ExpressDeliveryService.View
{
    public partial class ReadOrderView : UserControl
    {
        internal ReadOrderView(ReadOrderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
