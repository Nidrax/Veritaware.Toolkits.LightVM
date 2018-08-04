using Veritaware.Toolkits.LightVM.Net;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class OrderDetail : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => TryAndSet(value, ref _id);
        }


        private Product _product;

        public Product Product
        {
            get => _product;
            set => TryAndSet(value, ref _product);
        }


        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set => TryAndSet(value, ref _quantity);
        }


        public decimal Price => (Product?.UnitPrice * Quantity) ?? 0;
    }
}
