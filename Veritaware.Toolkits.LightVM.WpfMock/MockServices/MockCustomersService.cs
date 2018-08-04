using System.Collections.Generic;
using System.Linq;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.MockServices
{
    internal class MockCustomersService : ICustomersService
    {
        private readonly List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "Petronilla", LastName = "Banck", Address = "13163 Kinsman Parkway" },
            new Customer { Id = 2, FirstName = "Maryjane", LastName = "Jugging", Address = "6102 Vera Crossing" },
            new Customer { Id = 3, FirstName = "Nevile", LastName = "Clendening", Address = "4 Sundown Center" },
            new Customer { Id = 4, FirstName = "Austen", LastName = "Pecht", Address = "97 Transport Crossing" },
            new Customer { Id = 5, FirstName = "Lyndell", LastName = "Harman", Address = "47972 Aberg Point" },
            new Customer { Id = 6, FirstName = "James", LastName = "Wigfield", Address = "63 Kings Circle" },
            new Customer { Id = 7, FirstName = "Tracie", LastName = "Flannigan", Address = "7702 Kensington Crossing" },
            new Customer { Id = 8, FirstName = "Torr", LastName = "Keedy", Address = "7 Lyons Avenue" },
            new Customer { Id = 9, FirstName = "Darby", LastName = "Lamonby", Address = "97297 Center Center" },
            new Customer { Id = 10, FirstName = "Alvira", LastName = "Dalrymple", Address = "56 Pankratz Hill" },
        };

        public void Add(Customer customer) => _customers.Add(customer);

        public void Delete(Customer customer)
        {
            if (_customers.Contains(customer))
                _customers.Remove(customer);
        }

        public Customer Get(int id) => _customers.FirstOrDefault(x => x.Id == id);

        public ICollection<Customer> Get() => _customers;

        public void Update(Customer customer)
        {
            var c = _customers.FirstOrDefault(x => x.Id == customer.Id);
            if (c is null) return;
            var i = _customers.IndexOf(c);

            _customers.Insert(i, customer);
            _customers.Remove(c);
        }
    }
}
