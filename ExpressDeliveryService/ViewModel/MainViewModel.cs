using System.ComponentModel;

namespace ExpressDeliveryService.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion
    }
}
