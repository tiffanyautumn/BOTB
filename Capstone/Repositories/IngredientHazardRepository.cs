using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class IngredientHazardRepository : BaseRepository, IIngredientHazardRepository
    {
        public IngredientHazardRepository(IConfiguration configuration) : base(configuration) { }
        public List<IngredientHazard> GetHazardsByIngredientId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT h.Name, h.Id AS HId, h.Description,
                              ih.[Case], ih.Id, i.Id AS IId
                       FROM IngredientHazard ih
                       LEFT JOIN Hazard h ON ih.HazardId = h.Id
                       LEFT JOIN Ingredient i ON i.Id = ih.IngredientId
                       WHERE i.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var hazards = new List<IngredientHazard>();
                        while (reader.Read())
                        {
                            hazards.Add(new IngredientHazard()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                IngredientId = DbUtils.GetInt(reader, "IId"),
                                Case = DbUtils.GetString(reader, "Case"),
                                Hazard = new Hazard()
                                {
                                    Id = DbUtils.GetInt(reader, "HId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                }
                            });
                        }
                        reader.Close();
                        return hazards;
                    }
                }
            }
        }

        public IngredientHazard GetIngredientHazardById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT ih.[Case], ih.Id, ih.HazardId, ih.IngredientId
                       FROM IngredientHazard ih
                       WHERE ih.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        IngredientHazard ingredientHazard = null;
                        while (reader.Read())
                        {
                            if (ingredientHazard == null)
                            {
                                ingredientHazard = new IngredientHazard()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    Case = DbUtils.GetString(reader, "Case"),
                                    HazardId = DbUtils.GetInt(reader, "HazardId")
                                };
                            }

                        }

                        return ingredientHazard;
                    }

                }
            }
        }

        public void UpdateIngredientHazard(IngredientHazard ingredientHazard)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE IngredientHazard
                            SET 
                                IngredientId = @ingredientId,
                                HazardId = @hazardId,
                                [Case] = @case
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@ingredientId", ingredientHazard.IngredientId);
                    DbUtils.AddParameter(cmd, "@id", ingredientHazard.Id);
                    DbUtils.AddParameter(cmd, "@hazardId", ingredientHazard.HazardId);
                    DbUtils.AddParameter(cmd, "@case", ingredientHazard.Case);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddIngredientHazard(IngredientHazard ingredientHazard)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO IngredientHazard (HazardId, IngredientId, [Case])
                    OUTPUT INSERTED.Id
                    VALUES (@hazardId, @ingredientId, @case)";
                    DbUtils.AddParameter(cmd, "@hazardId", ingredientHazard.HazardId);
                    DbUtils.AddParameter(cmd, "@ingredientId", ingredientHazard.IngredientId);
                    DbUtils.AddParameter(cmd, "@case", ingredientHazard.Case);
                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    ingredientHazard.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteIngredientHazard(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM IngredientHazard
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
