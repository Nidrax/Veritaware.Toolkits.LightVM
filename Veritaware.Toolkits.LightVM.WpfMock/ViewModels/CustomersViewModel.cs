using System.Collections.ObjectModel;
using Veritaware.Toolkits.LightVM.Net;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.MockServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.ViewModels
{
    internal class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomersService _customersService;

        public CustomersViewModel() : this(new MockCustomersService()) { }

        public CustomersViewModel(ICustomersService customersService)
        {
            _customersService = customersService;

            Load();
        }

        private void Load()
        {
            Customers = new ObservableCollection<Customer>(_customersService.Get());
        }



        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => TryAndSet(value, ref _customers);
        }

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => TryAndSet(value, ref _selectedCustomer);
        }
    }
}
