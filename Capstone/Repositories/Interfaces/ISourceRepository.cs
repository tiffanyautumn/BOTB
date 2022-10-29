﻿using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface ISourceRepository
    {
        List<Source> GetAll();
        void AddSource(Source source);
        void DeleteSource(int id);
        List<Source> GetAllByReviewId(int id);
        void UpdateSource(Source source);
    }
}