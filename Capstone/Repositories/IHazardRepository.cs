using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IHazardRepository
    {
        void AddHazard(Hazard hazard);
        void DeleteHazard(int id);
        List<Hazard> GetAll();
        Hazard GetHazardById(int id);
        void UpdateHazard(Hazard hazard);
    }
}