using System.Collections.Generic;
using System.Linq;
using Veritaware.Toolkits.LightVM.WpfMock.IServices;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.MockServices
{
    internal class MockProductsService : IProductsService
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Bouq All Italian - Primerba", CodeTag = "668-37-7721", UnitPrice = 16.36M },
            new Product { Id = 2, Name = "Sponge Cake Mix - Chocolate", CodeTag = "417-76-6862", UnitPrice = 15.68M },
            new Product { Id = 3, Name = "Cheese Cheddar Processed", CodeTag = "511-79-406", UnitPrice = 3.69M },
            new Product { Id = 4, Name = "Ice Cream - Super Sandwich", CodeTag = "895-75-0130", UnitPrice = 1.85M },
            new Product { Id = 5, Name = "Pasta - Bauletti, Chicken White", CodeTag = "790-44-4450", UnitPrice = 0.43M },
            new Product { Id = 6, Name = "Sambuca - Ramazzotti", CodeTag = "434-41-7448", UnitPrice = 3.39M },
            new Product { Id = 7, Name = "Tamarind Paste", CodeTag = "766-63-6692", UnitPrice = 11.92M },
            new Product { Id = 8, Name = "Sugar - Splenda Sweetener", CodeTag = "349-02-6709", UnitPrice = 8.84M },
            new Product { Id = 9, Name = "Amarula Cream", CodeTag = "193-84-4216", UnitPrice = 19.42M },
            new Product { Id = 10, Name = "Chicken - White Meat, No Tender", CodeTag = "483-72-0483", UnitPrice = 4.71M },
            new Product { Id = 11, Name = "Soup - Knorr, Chicken Gumbo", CodeTag = "319-93-3227", UnitPrice = 3.16M },
            new Product { Id = 12, Name = "Pork - Loin, Bone - In", CodeTag = "620-48-0329", UnitPrice = 18.43M },
            new Product { Id = 13, Name = "Wine - Hardys Bankside Shiraz", CodeTag = "276-57-8763", UnitPrice = 10.21M },
            new Product { Id = 14, Name = "Wine - Chateau Bonnet", CodeTag = "880-17-9796", UnitPrice = 19.79M },
            new Product { Id = 15, Name = "Soup - Beef Conomme, Dry", CodeTag = "286-08-5057", UnitPrice = 16.35M },
            new Product { Id = 16, Name = "Wine - Baron De Rothschild", CodeTag = "867-14-7143", UnitPrice = 16.80M },
            new Product { Id = 17, Name = "Onions - Cippolini", CodeTag = "726-47-7328", UnitPrice = 13.02M },
            new Product { Id = 18, Name = "Apples - Spartan", CodeTag = "718-04-2603", UnitPrice = 18.57M },
            new Product { Id = 19, Name = "Beef - Ground, Extra Lean, Fresh", CodeTag = "541-18-5709", UnitPrice = 7.67M },
            new Product { Id = 20, Name = "Mints - Striped Red", CodeTag = "102-27-8620", UnitPrice = 10.30M },
        };

        public void Add(Product product) => _products.Add(product);

        public void Delete(Product product)
        {
            if (_products.Contains(product))
                _products.Remove(product);
        }

        public Product Get(int id) => _products.FirstOrDefault(x => x.Id == id);

        public ICollection<Product> Get() => _products;

        public void Update(Product product)
        {
            var p = _products.FirstOrDefault(x => x.Id == product.Id);
            if (p is null) return;
            var i = _products.IndexOf(p);

            _products.Insert(i, product);
            _products.Remove(p);
        }
    }
}
