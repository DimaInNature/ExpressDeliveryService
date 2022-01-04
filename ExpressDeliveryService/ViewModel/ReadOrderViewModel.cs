using Data.Repositories;
using Data.Repositories.Abstract;
using Models;
using MVVM.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ExpressDeliveryService.ViewModel
{
    internal sealed class ReadOrderViewModel : BaseViewModel
    {
        internal ReadOrderViewModel(UserModel activeUser)
        {
            _currentUser = activeUser;

            InitializeRepositories();
            InitializeData();
        }

        private readonly UserModel _currentUser;

        #region Properties

        #region Data

        public List<OrderModel> Orders
        {
            get => _orders is null
                ? new List<OrderModel>()
                : _orders;
            set
            {
                _orders = value is null
                    ? new List<OrderModel>()
                    : value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private List<OrderModel> _orders;

        public OrderModel SelectedViewOrder
        {
            get => _selectedViewOrder;
            set
            {
                _selectedViewOrder = value;
                OnPropertyChanged(nameof(SelectedViewOrder));
            }
        }

        private OrderModel _selectedViewOrder;

        #endregion

        #region Repositories

        private IGenericRepository<OrderModel> _orderRepository;

        #endregion

        #endregion

        #region Other Logic

        private void InitializeRepositories()
        {
            _orderRepository = new EFGenericRepository<OrderModel>();
        }

        private void InitializeData()
        {
            Orders = _currentUser.IsAdmin == "True"
                ? _orderRepository.Get().ToList()
                : _orderRepository.Get(x => x.UserId == _currentUser.Id)
                .ToList();
        }

        #endregion
    }
}
