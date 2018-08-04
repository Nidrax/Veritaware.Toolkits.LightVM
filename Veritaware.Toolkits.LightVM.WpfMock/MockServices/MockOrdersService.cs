using System;
using System.Collections.Generic;
using System.Linq;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.MockServices
{
    internal class MockOrdersService : IOrdersService
    {
        private List<Order> _orders;
        private readonly ICustomersService _customersService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly Random _random;

        public MockOrdersService() : this(new MockCustomersService(), new MockOrderDetailsService()) { }

        public MockOrdersService(ICustomersService customersService, IOrderDetailsService orderDetailsService)
        {
            _customersService = customersService;
            _orderDetailsService = orderDetailsService;
            _random = new Random();

            GenerateMockData();
        }

        private void GenerateMockData()
        {
            _orders = new List<Order>();

            var rounds = _random.Next(10, 30);

            for (var i = 0; i < rounds; i++)
            {
                var order = new Order
                {
                    Id = i + 1,
                    Customer = _customersService.Get(_random.Next(1, 10)),
                    Details = _orderDetailsService.Get(null)
                };

                _orders.Add(order);
            }
        }

        public Order Get(int id) => _orders.FirstOrDefault(x => x.Id == id);

        public ICollection<Order> Get() => _orders;

        public void Add(Order order) => _orders.Add(order);

        public void Update(Order order)
        {
            var o = _orders.FirstOrDefault(x => x.Id == order.Id);
            if (o is null) return;
            var i = _orders.IndexOf(o);

            _orders.Insert(i, order);
            _orders.Remove(o);
        }

        public void Delete(Order order)
        {
            if (_orders.Contains(order))
                _orders.Remove(order);
        }
    }
}
