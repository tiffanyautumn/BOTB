using Capstone.Models;
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

    }
}
