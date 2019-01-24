using Veritaware.Toolkits.LightVM.Net;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Product : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }


        private string _name;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }


        private string _codeTag;

        public string CodeTag
        {
            get => _codeTag;
            set => Set(ref _codeTag, value);
        }


        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get => _unitPrice;
            set => Set(ref _unitPrice, value);
        }
    }
}
