using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetByFirebaseId(string fBId);
        void Add(UserProfile profile);

    }
}