using Common;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using MVVM.Command;
using MVVM.ViewModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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

            var lat = GoogleMapHelper.GetLatitudeByKeywords(street: MapStreet);

            var lng = GoogleMapHelper.GetLongitudeByKeywords(street: MapStreet);

            var marker = new GMapMarker(new PointLatLng(lat, lng))
            {
                Shape = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Purple
                }
            };

            control.Markers.Add(marker);

            control.ShowCenter = false;

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
