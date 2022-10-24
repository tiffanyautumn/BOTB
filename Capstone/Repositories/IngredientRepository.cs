using Capstone.Models;
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
                       SELECT i.Name, i.[Function], i.SafetyInfo, i.Id,
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
                                Function = DbUtils.GetString(reader, "Function"),
                                SafetyInfo = DbUtils.GetString(reader, "SafetyInfo"),

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
                       SELECT i.Name, i.[Function], i.SafetyInfo, i.Id,
                              ir.RateId, ir.Id AS IrId, ir.UserId, ir.Review, ir.Source, ir.DateReviewed,
                              up.FirstName, up.LastName,
                              r.Rating
                       FROM Ingredient i
                       LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                       LEFT JOIN UserProfile up ON up.Id = ir.UserId
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
                                    Function = DbUtils.GetString(reader, "Function"),
                                    SafetyInfo = DbUtils.GetString(reader, "SafetyInfo"),

                                };
                                if (DbUtils.IsNotDbNull(reader, "IrId"))
                                {
                                    ingredient.IngredientReview = new IngredientReview()
                                    {
                                        Id = DbUtils.GetInt(reader, "IrId"),
                                        IngredientId = DbUtils.GetInt(reader, "Id"),
                                        RateId = DbUtils.GetInt(reader, "RateId"),
                                        UserId = DbUtils.GetInt(reader, "UserId"),
                                        Review = DbUtils.GetString(reader, "Review"),
                                        Source = DbUtils.GetString(reader, "Source"),
                                        DateReviewed = DbUtils.GetDateTime(reader, "DateReviewed"),
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
                                        }
                                    };
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
                                [Function] = @function,
                                SafetyInfo = @safetyInfo
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    DbUtils.AddParameter(cmd, "@id", ingredient.Id);
                    DbUtils.AddParameter(cmd, "@function", ingredient.Function);
                    DbUtils.AddParameter(cmd, "@safetyInfo", ingredient.SafetyInfo);

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
                    INSERT INTO Ingredient ([Name], [Function], SafetyInfo)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @function, @safetyInfo)";
                    DbUtils.AddParameter(cmd, "@name", ingredient.Name);
                    DbUtils.AddParameter(cmd, "@function", ingredient.Function);
                    DbUtils.AddParameter(cmd, "@safetyInfo", ingredient.SafetyInfo);

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
                    SELECT i.Id, i.[Name], i.[Function], i.[SafetyInfo],
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
                                Function = DbUtils.GetString(reader, "Function"),
                                SafetyInfo = DbUtils.GetString(reader, "SafetyInfo"),

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
                Function = DbUtils.GetString(reader, "Function"),
                SafetyInfo = DbUtils.GetString(reader, "SafetyInfo"),
            };
        }
    }
}
