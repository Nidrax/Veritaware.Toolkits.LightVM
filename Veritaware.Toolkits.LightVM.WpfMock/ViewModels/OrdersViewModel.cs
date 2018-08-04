using System.Collections.ObjectModel;
using Veritaware.Toolkits.LightVM.Net;
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
            set => TryAndSet(value, ref _orders);
        }

        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                TryAndSet(value, ref _selectedOrder);

                if (value is null) return;

                Details = new ObservableCollection<OrderDetail>(value.Details);
            }
        }

        private ObservableCollection<OrderDetail> _details;

        public ObservableCollection<OrderDetail> Details
        {
            get => _details;
            set => TryAndSet(value, ref _details);
        }

        private OrderDetail _selectedDetail;

        public OrderDetail SelectedDetail
        {
            get => _selectedDetail;
            set => TryAndSet(value, ref _selectedDetail);
        }
    }
}
