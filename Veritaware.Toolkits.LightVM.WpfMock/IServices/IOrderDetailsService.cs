using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.IServices
{
    internal interface IOrderDetailsService
    {
        OrderDetail Get(int id);
        ICollection<OrderDetail> Get(Order order);
        void Add(Order order, OrderDetail detail);
        void Update(Order order, OrderDetail detail);
        void Delete(Order order, OrderDetail detail);
    }
}
