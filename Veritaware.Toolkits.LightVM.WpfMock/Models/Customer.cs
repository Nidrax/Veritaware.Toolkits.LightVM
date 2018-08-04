using Veritaware.Toolkits.LightVM.Net;

namespace Veritaware.Toolkits.LightVM.WpfMock.Models
{
    internal class Customer : ModelBase
    {
        private int _id;

        public int Id
        {
            get => _id;
            set => TryAndSet(value, ref _id);
        }


        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                TryAndSet(value, ref _firstName);
                RaisePropertyChanged(nameof(FullName));
            }
        }


        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                TryAndSet(value, ref _lastName);
                RaisePropertyChanged(nameof(FullName));
            }
        }


        public string FullName => $"{FirstName} {LastName}";


        private string _address;

        public string Address
        {
            get => _address;
            set => TryAndSet(value, ref _address);
        }
    }
}
