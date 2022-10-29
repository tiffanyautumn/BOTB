using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.Repositories.Interfaces
{
    public interface IProductReviewRepository
    {
        void AddProductReview(ProductReview productReview);
        void DeleteProductReview(int id);
        ProductReview GetProductReviewById(int id);
        List<ProductReview> GetProductReviewsByProductId(int id);
    }
}