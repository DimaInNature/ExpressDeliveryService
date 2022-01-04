using System.Windows;
using System.Windows.Input;

namespace ExpressDeliveryService.View
{
    public partial class MainView : Window
    {
        public MainView() => InitializeComponent();

        private void WindowDragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
