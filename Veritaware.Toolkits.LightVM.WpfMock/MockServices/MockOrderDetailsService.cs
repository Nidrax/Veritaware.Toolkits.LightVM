using System;
using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.MockServices
{
    internal class MockOrderDetailsService : IOrderDetailsService
    {
        private readonly IProductsService _productsService;
        private readonly Random _random;

        public MockOrderDetailsService() : this(new MockProductsService()) { }

        public MockOrderDetailsService(IProductsService productsService)
        {
            _productsService = productsService;
            _random = new Random();
        }

        public void Add(Order order, OrderDetail detail) { }

        public void Delete(Order order, OrderDetail detail) { }

        public OrderDetail Get(int id)
        {
            var detail = new OrderDetail()
            {
                Id = id,
                Product = _productsService.Get(_random.Next(1, 20)),
                Quantity = _random.Next(5, 500)
            };

            return detail;
        }

        public ICollection<OrderDetail> Get(Order order)
        {
            var details = new List<OrderDetail>();
            var rounds = _random.Next(1, 15);

            for (var i = 0; i < rounds; i++)
            {
                var detail = new OrderDetail()
                {
                    Id = _random.Next(1, 5000),
                    Product = _productsService.Get(_random.Next(1, 20)),
                    Quantity = _random.Next(5, 500)
                };

                details.Add(detail);
            }

            return details;
        }

        public void Update(Order order, OrderDetail detail) { }
    }
}
