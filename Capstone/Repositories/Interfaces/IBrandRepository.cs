using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        void AddBrand(Brand brand);
        List<Brand> GetAll();
        Brand GetBrandById(int id);

        void UpdateBrand(Brand brand);
        void DeleteBrand(int id);

    }
}