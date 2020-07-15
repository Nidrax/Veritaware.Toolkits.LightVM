using System.Collections.Generic;
using System.Linq;
using Veritaware.Toolkits.LightVM;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Order : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }


        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set => Set(ref _customer, value);
        }


        private ICollection<OrderDetail> _details;

        public ICollection<OrderDetail> Details
        {
            get => _details;
            set => Set(ref _details, value);
        }


        public decimal TotalPrice => Details.Sum(x => x.Price);
    }
}
