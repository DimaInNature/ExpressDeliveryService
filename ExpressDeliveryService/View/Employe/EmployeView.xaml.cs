using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.View.Employe
{
    public partial class EmployeView : Window
    {
        public EmployeView() => InitializeComponent();

        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
