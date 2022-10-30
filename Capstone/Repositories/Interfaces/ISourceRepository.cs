using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface ISourceRepository
    {
        List<Source> GetAll();
        Source GetSourceById(int id);

        void AddSource(Source source);
        void DeleteSource(int id);
        List<Source> GetAllByReviewId(int id);
        void UpdateSource(Source source);
    }
}