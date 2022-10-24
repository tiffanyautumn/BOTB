using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
                       SELECT i.Name, i.[Function], i.SafetyInfo, i.Id AS IngredientId,
                              ir.RateId, ir.Id , ir.UserId, ir.Review, ir.Source, ir.DateReviewed,
                              up.FirstName, up.LastName,
                              r.Rating
                       FROM IngredientReview ir
                       LEFT JOIN Ingredient i ON ir.IngredientId = i.Id
                       LEFT JOIN UserProfile up ON up.Id = ir.UserId
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
                                    Source = DbUtils.GetString(reader, "Source"),
                                    DateReviewed = DbUtils.GetDateTime(reader, "DateReviewed"),
                                    UserId = DbUtils.GetInt(reader, "UserId"),
                                    RateId = DbUtils.GetInt(reader, "RateId"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserId"),
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
                                        Name = DbUtils.GetString(reader, "Name"),
                                        Function = DbUtils.GetString(reader, "Function"),
                                        SafetyInfo = DbUtils.GetString(reader, "SafetyInfo"),

                                    }

                                };
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
                                UserId = @UserId,
                                IngredientId = @ingredientId,
                                Review = @review,
                                Source = @source,
                                DateReviewed = @dateReviewed
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@rateId", ingredientReview.RateId);
                    DbUtils.AddParameter(cmd, "@Userid", ingredientReview.UserId);
                    DbUtils.AddParameter(cmd, "@IngredientId", ingredientReview.IngredientId);
                    DbUtils.AddParameter(cmd, "@review", ingredientReview.Review);
                    DbUtils.AddParameter(cmd, "@source", ingredientReview.Source);
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
                    INSERT INTO IngredientReview (RateId, UserId, IngredientId, Review, Source, DateReviewed)
                    OUTPUT INSERTED.Id
                    VALUES (@rateId, @userId, @ingredientId, @review, @source, @dateReviewed)";
                    DbUtils.AddParameter(cmd, "@rateId", ingredientReview.RateId);
                    DbUtils.AddParameter(cmd, "@userId", ingredientReview.UserId);
                    DbUtils.AddParameter(cmd, "@ingredientId", ingredientReview.IngredientId);
                    DbUtils.AddParameter(cmd, "@review", ingredientReview.Review);
                    DbUtils.AddParameter(cmd, "@source", ingredientReview.Source);
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
