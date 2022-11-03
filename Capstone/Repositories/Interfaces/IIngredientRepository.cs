using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        void AddIngredient(Ingredient ingredient);
        void DeleteIngredient(int id);
        List<Ingredient> GetAll();
        Ingredient GetIngredientById(int id);
        void UpdateIngredient(Ingredient ingredient);
        List<Ingredient> Search(string criterion);
    }
}