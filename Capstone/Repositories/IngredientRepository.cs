using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class IngredientRepository : BaseRepository, IIngredientRepository
    {
        public IngredientRepository(IConfiguration configuration) : base(configuration) { }

        public List<Ingredient> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT i.Name, i.Id, i.ImageUrl,
                              ir.RateId, ir.Id AS IrId, r.Rating
                       FROM Ingredient i
                       LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                       LEFT JOIN Rate r ON r.Id = ir.RateId
                       ORDER BY i.Name";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var ingredients = new List<Ingredient>();

                        while (reader.Read())
                        {
                            Ingredient ingredient= new Ingredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl")

                            };
                            if (DbUtils.IsNotDbNull(reader, "IrId"))
                            {
                                ingredient.IngredientReview = new IngredientReview()
                                {
                                    Id = DbUtils.GetInt(reader, "IrId"),
                                    Rate = new Rate()
                                    {
                                        Id = DbUtils.GetInt(reader, "RateId"),
                                        Rating = DbUtils.GetString(reader, "Rating")
                                    }
                                };
                            }
                            ingredients.Add(ingredient);

                        };
                        reader.Close();
                        return ingredients;

                    }


                }
            }
        }


        public Ingredient GetIngredientById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT i.Name, i.Id,i.ImageUrl,
                              ir.RateId, ir.Id AS IrId, ir.UserProfileId, ir.Review, ir.DateReviewed,
                              s.Link, s.Id AS SourceId,
                              u.Id AS UseId, u.Description,
                              ih.Id AS IHId, ih.[Case],
                              h.Id AS HId, h.Name AS HName, H.Description AS HDescription,
                              up.FirstName, up.LastName,
                              r.Rating
                       FROM Ingredient i
                       LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                       LEFT JOIN Source s ON s.IngredientReviewId = ir.Id
                       LEFT JOIN UserProfile up ON up.Id = ir.UserProfileId
                       LEFT JOIN IngredientHazard ih ON ih.IngredientId = i.Id
                       LEFT JOIN Hazard h ON ih.HazardId = h.Id
                       LEFT JOIN [Use] u ON u.IngredientId = i.Id
                       LEFT JOIN Rate r ON r.Id = ir.RateId
                       WHERE i.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Ingredient ingredient = null;
                        while (reader.Read())
                        {
                            if (ingredient == null)
                            {
                                ingredient = new Ingredient()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    ImageUrl= DbUtils.GetString(reader, "ImageUrl"),
                                    Uses = new List<Use>(),
                                    Hazards = new List<Models.IngredientHazard>()

                                };
                                if (DbUtils.IsNotDbNull(reader, "IrId"))
                                {
                                    ingredient.IngredientReview = new IngredientReview()
                                    {
                                        Id = DbUtils.GetInt(reader, "IrId"),
                                        IngredientId = DbUtils.GetInt(reader, "Id"),
                                        RateId = DbUtils.GetInt(reader, "RateId"),
                                        UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                        Review = DbUtils.GetString(reader, "Review"),
                                        DateReviewed = DbUtils.GetDateTime(reader, "DateReviewed"),
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
                                        Sources = new List<Source>()
                                    };
                                    if(DbUtils.IsNotDbNull(reader, "SourceId"))
                                       {
                                        ingredient.IngredientReview.Sources.Add(new Source()
                                        {
                                            Id = DbUtils.GetInt(reader, "SourceId"),
                                            Link = DbUtils.GetString(reader, "Link"),
                                            IngredientReviewId = DbUtils.GetInt(reader, "IrId")
                                        });

                                        }
                                }
                                if(DbUtils.IsNotDbNull(reader, "UseId"))
                                {
                                    ingredient.Uses.Add(new Use()
                                    {
                                        Id = DbUtils.GetInt(reader, "UseId"),
                                        Description = DbUtils.GetString(reader, "Description")
                                    });
                                }
                                if(DbUtils.IsNotDbNull(reader, "IHId"))
                                {
                                    ingredient.Hazards.Add(new Models.IngredientHazard()
                                    {
                                        Id = DbUtils.GetInt(reader, "IHId"),
                                        HazardId = DbUtils.GetInt(reader, "HId"),
                                        Case = DbUtils.GetString(reader, "Case"),
                                        Hazard = new Hazard()
                                        {
                                            Id = DbUtils.GetInt(reader, "HId"),
                                            Name = DbUtils.GetString(reader, "HName"),
                                            Description = DbUtils.GetString(reader, "HDescription")
                                        }
                                    });
                                }
                            }
                        }
                        return ingredient;

                    }
                }
            }
        }


        public void UpdateIngredient(Ingredient ingredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Ingredient
                            SET 
                                [Name] = @name,
                                ImageUrl = @imageUrl
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    DbUtils.AddParameter(cmd, "@id", ingredient.Id);
                    DbUtils.AddParameter(cmd, "@imageUrl", ingredient.ImageUrl);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Ingredient ([Name], ImageUrl)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @imageUrl)";
                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    DbUtils.AddParameter(cmd, "@imageUrl", ingredient.ImageUrl);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    ingredient.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteIngredient(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Ingredient
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Ingredient> Search(string criterion)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql = @"
                    SELECT i.Name, i.Id, i.ImageUrl,
                           ir.RateId, ir.Id AS IrId, r.Rating
                    FROM Ingredient i
                    LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                    LEFT JOIN Rate r ON r.Id = ir.RateId
                    WHERE i.[Name] LIKE @Criterion
                    ORDER BY i.[Name]";
                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var ingredients = new List<Ingredient>();
                        while (reader.Read())
                        {
                            Ingredient ingredient = new Ingredient()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),

                            };
                            if (DbUtils.IsNotDbNull(reader, "IrId"))
                            {
                                ingredient.IngredientReview = new IngredientReview()
                                {
                                    Id = DbUtils.GetInt(reader, "IrId"),
                                    Rate = new Rate()
                                    {
                                        Id = DbUtils.GetInt(reader, "RateId"),
                                        Rating = DbUtils.GetString(reader, "Rating")
                                    }
                                };
                            }
                            ingredients.Add(ingredient);
                        }
                        return ingredients;
                    }
                }
            }
        }

        private Ingredient NewIngredientFromReader(SqlDataReader reader)
        {
            return new Ingredient
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
            };
        }
    }
}
