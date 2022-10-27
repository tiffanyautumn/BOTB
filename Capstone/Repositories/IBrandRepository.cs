using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IBrandRepository
    {
        void AddBrand(Brand brand);
        List<Brand> GetAll();
    }
}