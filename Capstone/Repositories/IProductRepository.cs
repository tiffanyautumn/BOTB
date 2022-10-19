using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
        void DeleteProduct(int id);

    }
}