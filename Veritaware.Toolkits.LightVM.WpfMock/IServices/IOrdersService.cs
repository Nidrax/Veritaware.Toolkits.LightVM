using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.IServices
{
    internal interface IOrdersService
    {
        Order Get(int id);
        ICollection<Order> Get();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
