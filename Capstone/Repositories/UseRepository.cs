using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class UseRepository : BaseRepository, IUseRepository
    {
        public UseRepository(IConfiguration configuration) : base(configuration) { }

        public Use GetUseById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.Id, u.Description, u.IngredientId
                       FROM [Use] u
                       WHERE u.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Use use = null;
                        while (reader.Read())
                        {
                            if (use == null)
                            {
                                use = (new Use()
                                {
                                    Id = id,
                                    Description = DbUtils.GetString(reader, "Description"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId")
                                });
                            }


                        }


                        return use;
                    }

                }
            }
        }

        public List<Use> GetAllIngredientUses(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT u.Id, u.Description, u.IngredientId
                       FROM [Use] u
                       WHERE u.IngredientId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var uses = new List<Use>();

                        while (reader.Read())
                        {
                            uses.Add(new Use()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                    IngredientId = id
                                });
                        }

                        return uses;
                    }

                }
            }
        }


        public void AddUse(Use use)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Use] (Description, IngredientId)
                    OUTPUT INSERTED.Id
                    VALUES (@description, @ingredientId)";
                    DbUtils.AddParameter(cmd, "@description", use.Description);
                    DbUtils.AddParameter(cmd, "@ingredientId", use.IngredientId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    use.Id = newlyCreatedId;
                }
            }
        }

        public void AddProductIngredientUse(ProductIngredientUse use)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO ProductIngredientUse (UseId, ProductIngredientId)
                    OUTPUT INSERTED.Id
                    VALUES (@useId, @productIngredientId)";
                    DbUtils.AddParameter(cmd, "@useId", use.UseId);
                    DbUtils.AddParameter(cmd, "@productIngredientId", use.ProductIngredientId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    use.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteUse(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [Use]
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
