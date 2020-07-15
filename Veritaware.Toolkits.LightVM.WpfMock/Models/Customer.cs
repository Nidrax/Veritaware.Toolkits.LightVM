using Veritaware.Toolkits.LightVM;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Customer : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }


        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value);
                RaisePropertyChanged(nameof(FullName));
            }
        }


        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value);
                RaisePropertyChanged(nameof(FullName));
            }
        }


        public string FullName => $"{FirstName} {LastName}";


        private string _address;

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }
    }
}
