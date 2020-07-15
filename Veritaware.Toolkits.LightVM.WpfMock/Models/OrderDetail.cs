using Veritaware.Toolkits.LightVM;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class OrderDetail : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }


        private Product _product;

        public Product Product
        {
            get => _product;
            set => Set(ref _product, value);
        }


        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set => Set(ref _quantity, value);
        }


        public decimal Price => (Product?.UnitPrice * Quantity) ?? 0;
    }
}
