using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IRateRepository
    {
        List<Rate> GetAll();
        Rate GetRateById(int id);
        void AddRate(Rate rate);
        void DeleteRate(int id);
        void UpdateRate(Rate rate);


    }
}