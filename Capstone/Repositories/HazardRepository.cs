using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class HazardRepository : BaseRepository, IHazardRepository
    {
        public HazardRepository(IConfiguration configuration) : base(configuration) { }

        public List<Hazard> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT h.Id, h.[Name], h.Description
                       FROM Hazard h
                       ORDER BY h.[Name]";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var hazards = new List<Hazard>();

                        while (reader.Read())
                        {
                            hazards.Add(new Hazard()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Description = DbUtils.GetString(reader, "Description"),
                            });

                        }
                        reader.Close();
                        return hazards;

                    }
                }
            }
        }

       

        public Hazard GetHazardById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT h.Name, h.Id, h.Description
                       FROM Hazard h
                       WHERE h.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Hazard hazard = null;
                        while (reader.Read())
                        {
                            if (hazard == null)
                            {
                                hazard = new Hazard()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Description = DbUtils.GetString(reader, "Description"),
                                };
                            }

                        }

                        return hazard;
                    }

                }
            }
        }

        public void UpdateHazard(Hazard hazard)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Hazard
                            SET 
                                [Name] = @name,
                                Description = @description
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", hazard.Name);
                    DbUtils.AddParameter(cmd, "@id", hazard.Id);
                    DbUtils.AddParameter(cmd, "@description", hazard.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddHazard(Hazard hazard)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Hazard (Name, Description)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @description)";
                    DbUtils.AddParameter(cmd, "@name", hazard.Name);
                    DbUtils.AddParameter(cmd, "@description", hazard.Description);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    hazard.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteHazard(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Hazard
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
