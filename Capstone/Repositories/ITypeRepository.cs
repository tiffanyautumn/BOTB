using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public interface ITypeRepository
    {
        List<Type> GetAll();
        void AddType(Type type);
    }
}