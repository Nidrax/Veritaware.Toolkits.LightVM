using System.Collections.ObjectModel;
using Veritaware.Toolkits.LightVM;
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
            set => Set(ref _customers, value);
        }

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => Set(ref _selectedCustomer, value);
        }
    }
}
