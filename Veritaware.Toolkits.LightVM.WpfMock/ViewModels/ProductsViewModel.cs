using System.Collections.ObjectModel;
using Veritaware.Toolkits.LightVM;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.MockServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.ViewModels
{
    internal class ProductsViewModel : ViewModelBase
    {
        private readonly IProductsService _productsService;

        public ProductsViewModel() : this(new MockProductsService()) { }

        public ProductsViewModel(IProductsService productsService)
        {
            _productsService = productsService;

            Load();
        }

        private void Load()
        {
            Products = new ObservableCollection<Product>(_productsService.Get());
        }



        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
    }
}
