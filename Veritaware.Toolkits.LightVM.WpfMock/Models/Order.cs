using System.Collections.Generic;
using System.Linq;
using Veritaware.Toolkits.LightVM.Net;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Order : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => TryAndSet(value, ref _id);
        }


        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set => TryAndSet(value, ref _customer);
        }


        private ICollection<OrderDetail> _details;

        public ICollection<OrderDetail> Details
        {
            get => _details;
            set => TryAndSet(value, ref _details);
        }


        public decimal TotalPrice => Details.Sum(x => x.Price);
    }
}
