using Capstone.Models;

namespace Capstone.Repositories
{
    public interface IProductIngredientRepository
    {
        void AddProductIngredient(ProductIngredient productIngredient);
        void DeleteProductIngredient(int id);
        ProductIngredient GetProductIngredientById(int id);
        void UpdateProductIngredient(ProductIngredient productIngredient);
    }
}