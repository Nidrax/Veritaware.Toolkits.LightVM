using Veritaware.Toolkits.LightVM.Net;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Product : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => TryAndSet(value, ref _id);
        }


        private string _name;

        public string Name
        {
            get => _name;
            set => TryAndSet(value, ref _name);
        }


        private string _codeTag;

        public string CodeTag
        {
            get => _codeTag;
            set => TryAndSet(value, ref _codeTag);
        }


        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get => _unitPrice;
            set => TryAndSet(value, ref _unitPrice);
        }
    }
}
