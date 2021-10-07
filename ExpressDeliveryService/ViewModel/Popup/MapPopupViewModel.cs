using ExpressDeliveryService.Services.Command;
using ExpressDeliveryService.ViewModel.Base;
using System.Windows.Input;

namespace ExpressDeliveryService.ViewModel.Popup
{
    public sealed class MapPopupViewModel : ViewModelBase
    {
        public MapPopupViewModel() { }

        public MapPopupViewModel(string street)
        {
            _mapStreet = street;

            InitialCommands();
        }

        #region Properties

        #region Commands

        public ICommand MapLoadedCommand { get; private set; }

        #endregion

        #region Map Props

        public GMap.NET.MapProviders.OpenStreetMapProvider MapProvider => _mapProvider;

        private GMap.NET.MapProviders.OpenStreetMapProvider _mapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;

        public string MapStreet
        {
            get => _mapStreet;
            set
            {
                _mapStreet = value;
                OnPropertyChanged("MapStreet");
            }
        }

        private string _mapStreet;

        #endregion

        #endregion

        #region Methods

        #region Commands

        private void MapLoaded(object obj)
        {
            var control = obj as GMap.NET.WindowsPresentation.GMapControl;
            control.DragButton = MouseButton.Left;
            control.SetPositionByKeywords(MapStreet);
        }

        #endregion

        public void InitialCommands()
        {
            MapLoadedCommand = new DelegateCommandService(MapLoaded);
        }

        #endregion

    }
}
