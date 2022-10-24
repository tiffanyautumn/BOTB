using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface IRateRepository
    {
        List<Rate> GetAll();
    }
}