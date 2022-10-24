using Capstone.Models;

namespace Capstone.Repositories
{
    public interface IIngredientReviewRepository
    {
        void AddIngredientReview(IngredientReview ingredientReview);
        void DeleteIngredientReview(int id);
        void UpdateIngredientReview(IngredientReview ingredientReview);
        IngredientReview GetIngredientReviewById(int id);

    }
}