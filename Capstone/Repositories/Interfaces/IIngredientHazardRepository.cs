using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IIngredientHazardRepository
    {
        void AddIngredientHazard(IngredientHazard ingredientHazard);
        void DeleteIngredientHazard(int id);
        List<IngredientHazard> GetHazardsByIngredientId(int id);
        IngredientHazard GetIngredientHazardById(int id);
        void UpdateIngredientHazard(IngredientHazard ingredientHazard);
    }
}