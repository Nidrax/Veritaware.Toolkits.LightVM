using System.Collections.Generic;
using Veritaware.Toolkits.LightVM.WpfMock.Models;

namespace Veritaware.Toolkits.LightVM.WpfMock.IServices
{
    internal interface IProductsService
    {
        Product Get(int id);
        ICollection<Product> Get();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
