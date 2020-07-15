using System.Collections.ObjectModel;
using Veritaware.Toolkits.LightVM;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.MockServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.ViewModels
{
    internal class OrdersViewModel : ViewModelBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersViewModel() : this(new MockOrdersService()) { }

        public OrdersViewModel(IOrdersService ordersService)
        {
            _ordersService = ordersService;

            Load();
        }

        private void Load()
        {
            Orders = new ObservableCollection<Order>(_ordersService.Get());
        }



        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => Set(ref _orders, value);
        }

        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                Set(ref _selectedOrder, value);

                if (value is null) return;

                Details = new ObservableCollection<OrderDetail>(value.Details);
            }
        }

        private ObservableCollection<OrderDetail> _details;

        public ObservableCollection<OrderDetail> Details
        {
            get => _details;
            set => Set(ref _details, value);
        }

        private OrderDetail _selectedDetail;

        public OrderDetail SelectedDetail
        {
            get => _selectedDetail;
            set => Set(ref _selectedDetail, value);
        }
    }
}
