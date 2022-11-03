using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class RateRepository : BaseRepository, IRateRepository
    {
        public RateRepository(IConfiguration configuration) : base(configuration) { }

        public List<Rate> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Rating, Id
                       FROM Rate";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var rates = new List<Rate>();

                        while (reader.Read())
                        {
                            rates.Add(new Rate()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Rating = DbUtils.GetString(reader, "Rating"),
                            });


                        };
                        reader.Close();
                        return rates;

                    }


                }
            }
        }
        public Rate GetRateById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT r.Rating, r.Id
                       FROM [Rate] r
                       WHERE r.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Rate rate = null;
                        while (reader.Read())
                        {
                            if (rate == null)
                            {
                                rate = new Rate()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Rating = DbUtils.GetString(reader, "Rating")
                                };

                            }
                        }
                        return rate;

                    }
                }
            }
        }
        public void AddRate(Rate rate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Rate] ([Rating])
                    OUTPUT INSERTED.Id
                    VALUES (@rating)";
                    DbUtils.AddParameter(cmd, "@rating", rate.Rating);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    rate.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteRate(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [Rate]
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRate(Rate rate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Rate]
                            SET 
                                [Rating] = @rating
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@rating", rate.Rating);
                    DbUtils.AddParameter(cmd, "@id", rate.Id);


                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
