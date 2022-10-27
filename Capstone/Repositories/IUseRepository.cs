using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IUseRepository
    {
        void AddUse(Use use);
        Use GetUseById(int id);
        List<Use> GetAllIngredientUses(int id);
        void DeleteUse(int id);
    }
}