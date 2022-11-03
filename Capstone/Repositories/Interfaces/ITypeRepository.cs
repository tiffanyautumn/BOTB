using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        List<Type> GetAll();
        Type GetTypeById(int id);
        void AddType(Type type);
        void UpdateType(Type type);
        void DeleteType(int id);
    }
}