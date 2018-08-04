using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.IServices
{
    internal interface ICustomersService
    {
        Customer Get(int id);
        ICollection<Customer> Get();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
