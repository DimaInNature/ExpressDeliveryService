using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MVVM.Command;
using MVVM.ViewModel;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Popup
{
    internal sealed class MapPopupViewModel : BaseViewModel
    {
        internal MapPopupViewModel() { }

        internal MapPopupViewModel(string streetSource)
        {
            InitializeCommands();
            SetCondition(source: streetSource);
        }

        #region Properties

        public OpenStreetMapProvider MapProvider { get; private set; }

        public string MapStreet
        {
            get => _mapStreet;
            set
            {
                _mapStreet = value;
                OnPropertyChanged(nameof(MapStreet));
            }
        }

        private string _mapStreet;

        #endregion

        #region Commands

        public ICommand MapLoadedCommand { get; private set; }

        #endregion

        #region Commands Methods

        private void MapLoaded(object obj)
        {
            var control = obj as GMapControl;

            control.DragButton = MouseButton.Left;

            control.SetPositionByKeywords(MapStreet);
        }

        #endregion

        #region Other Methods

        private void InitializeCommands()
        {
            MapLoadedCommand = new RelayCommand(MapLoaded);
        }

        private void SetCondition(string source)
        {
            MapProvider = OpenStreetMapProvider.Instance;
            MapStreet = source;
        }

        #endregion
    }
}
