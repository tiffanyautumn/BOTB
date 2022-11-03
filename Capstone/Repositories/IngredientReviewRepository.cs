using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class IngredientReviewRepository : BaseRepository, IIngredientReviewRepository
    {
        public IngredientReviewRepository(IConfiguration configuration) : base(configuration) { }

        public IngredientReview GetIngredientReviewById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT i.Name, i.Id AS IngredientId,
                              ir.RateId, ir.Id , ir.UserProfileId, ir.Review, ir.DateReviewed,
                              s.Id AS SId, s.Link,
                              up.FirstName, up.LastName,
                              r.Rating
                       FROM IngredientReview ir
                       LEFT JOIN Ingredient i ON ir.IngredientId = i.Id
                       LEFT JOIN UserProfile up ON up.Id = ir.UserProfileId
                       LEFT JOIN Source s ON s.IngredientReviewId = ir.Id
                       LEFT JOIN Rate r ON r.Id = ir.RateId
                       WHERE ir.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        IngredientReview ingredientReview = null;
                        while (reader.Read())
                        {
                            if (ingredientReview == null)
                            {
                                ingredientReview = new IngredientReview()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Review = DbUtils.GetString(reader, "Review"),
                                    DateReviewed = DbUtils.GetDateTime(reader, "DateReviewed"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    RateId = DbUtils.GetInt(reader, "RateId"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserProfileId"),
                                        FirstName = DbUtils.GetString(reader, "FirstName"),
                                        LastName = DbUtils.GetString(reader, "LastName")
                                    },
                                    Rate = new Rate()
                                    {
                                        Id = DbUtils.GetInt(reader, "RateId"),
                                        Rating = DbUtils.GetString(reader, "Rating")
                                    },
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "Name")  
                                    },
                                    Sources = new List<Source>()

                                };
                            }
                            if(DbUtils.IsNotDbNull(reader,"SId"))
                            {
                                ingredientReview.Sources.Add(new Source()
                                {
                                    Id = DbUtils.GetInt(reader, "SId"),
                                    Link = DbUtils.GetString(reader, "Link")
                                });
                            }
                        }
                        return ingredientReview;
                    }
                }
            }
        }
        public void UpdateIngredientReview(IngredientReview ingredientReview)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE IngredientReview
                            SET 
                                RateId = @rateId,
                                UserProfileId = @UserProfileId,
                                IngredientId = @ingredientId,
                                Review = @review,
                                DateReviewed = @dateReviewed
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@rateId", ingredientReview.RateId);
                    DbUtils.AddParameter(cmd, "@UserProfileid", ingredientReview.UserProfileId);
                    DbUtils.AddParameter(cmd, "@IngredientId", ingredientReview.IngredientId);
                    DbUtils.AddParameter(cmd, "@review", ingredientReview.Review);
                    DbUtils.AddParameter(cmd, "@dateReviewed", ingredientReview.DateReviewed);
                    DbUtils.AddParameter(cmd, "@id", ingredientReview.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddIngredientReview(IngredientReview ingredientReview)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO IngredientReview (RateId, UserProfileId, IngredientId, Review, DateReviewed)
                    OUTPUT INSERTED.Id
                    VALUES (@rateId, @userId, @ingredientId, @review,@dateReviewed)";
                    DbUtils.AddParameter(cmd, "@rateId", ingredientReview.RateId);
                    DbUtils.AddParameter(cmd, "@userId", ingredientReview.UserProfileId);
                    DbUtils.AddParameter(cmd, "@ingredientId", ingredientReview.IngredientId);
                    DbUtils.AddParameter(cmd, "@review", ingredientReview.Review);
                    DbUtils.AddParameter(cmd, "@dateReviewed", ingredientReview.DateReviewed);
                    
                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    ingredientReview.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteIngredientReview(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM IngredientReview
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
