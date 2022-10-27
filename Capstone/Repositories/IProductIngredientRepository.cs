using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IProductIngredientRepository
    {
        void AddProductIngredient(ProductIngredient productIngredient);
        void DeleteProductIngredient(int id);
        ProductIngredient GetProductIngredientById(int id);
        void UpdateProductIngredient(ProductIngredient productIngredient);

        List<ProductIngredient> GetProductIngredientsByProductId(int id);
    }
}