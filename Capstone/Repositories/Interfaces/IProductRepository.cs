using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product GetProductById(int id);
        void UpdateProduct(Product product);
        void AddProduct(Product product);
        void DeleteProduct(int id);
        List<Product> Search(string criterion);

        void AddUserProduct(Product product, int id);

        Product GetUserProductById(int id);
        List<UserProduct> GetUserProductsByUserId(int id);
        void DeleteUserProduct(int id);
    }
}