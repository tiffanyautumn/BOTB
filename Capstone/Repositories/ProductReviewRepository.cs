using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class ProductReviewRepository : BaseRepository, IProductReviewRepository
    {
        public ProductReviewRepository(IConfiguration configuration) : base(configuration) { }
        public ProductReview GetProductReviewById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT pr.Id, pr.UserProfileId, pr.ProductId, pr.Comment, pr.AffordabilityRate, pr.EfficacyRate, pr.OverallRate,
                              up.FirstName, up.LastName
                       FROM ProductReview pr
                       LEFT JOIN UserProfile up ON pr.UserProfileId = up.Id
                       WHERE p.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ProductReview productReview = null;
                        while (reader.Read())
                        {
                            if (productReview == null)
                            {
                                productReview = new ProductReview()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserProfileId"),
                                        FirstName = DbUtils.GetString(reader, "FirstName"),
                                        LastName = DbUtils.GetString(reader, "LastName")
                                    },
                                    Comment = DbUtils.GetString(reader, "Comment"),
                                    AffordabilityRate = DbUtils.GetInt(reader, "AffordabilityRate"),
                                    EfficacyRate = DbUtils.GetInt(reader, "EfficacyRate"),
                                    OverallRate = DbUtils.GetInt(reader, "OverallRate")

                                };
                            }

                        }
                        return productReview;
                    }
                }
            }
        }
        public List<ProductReview> GetProductReviewsByProductId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT p.Id,
                              pr.Id AS ReviewId, pr.UserProfileId, pr.ProductId, pr.Comment, pr.AffordabilityRate, pr.EfficacyRate, pr.OverallRate,
                              up.FirstName, up.LastName
                       FROM ProductReview pr 
                       LEFT JOIN Product p ON p.Id = pr.ProductId
                       LEFT JOIN UserProfile up ON pr.UserProfileId = up.Id
                       WHERE p.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var reviews = new List<ProductReview>();

                        while (reader.Read())
                        {

                            reviews.Add(new ProductReview
                            {
                                Id = DbUtils.GetInt(reader, "ReviewId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName")
                                },
                                ProductId = DbUtils.GetInt(reader, "Id"),
                                Comment = DbUtils.GetString(reader, "Comment"),
                                AffordabilityRate = DbUtils.GetInt(reader, "AffordabilityRate"),
                                EfficacyRate = DbUtils.GetInt(reader, "EfficacyRate"),
                                OverallRate = DbUtils.GetInt(reader, "OverallRate")
                            });

                        }
                        return reviews;

                    }
                }
            }
        }

        public void AddProductReview(ProductReview productReview)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO ProductReview (UserProfileId, ProductId, Comment, AffordabilityRate, EfficacyRate, OverallRate)
                    OUTPUT INSERTED.Id
                    VALUES (@userProfileId, @productId, @comment, @affordabilityRate, @efficacyRate, @overallRate)";
                    DbUtils.AddParameter(cmd, "@userProfileId", productReview.UserProfileId);
                    DbUtils.AddParameter(cmd, "@comment", productReview.Comment);
                    DbUtils.AddParameter(cmd, "@affordabilityRate", productReview.AffordabilityRate);
                    DbUtils.AddParameter(cmd, "@efficacyRate", productReview.EfficacyRate);
                    DbUtils.AddParameter(cmd, "@overallRate", productReview.OverallRate);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    productReview.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteProductReview(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProductReview
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
