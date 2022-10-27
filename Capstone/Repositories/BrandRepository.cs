using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Repositories
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public BrandRepository(IConfiguration configuration) : base(configuration) { }

        public List<Brand> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Name, Id
                        From Brand";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var brands = new List<Brand>();
                        while (reader.Read())
                        {
                            brands.Add(new Brand()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            });
                        }
                        reader.Close();
                        return brands;
                    }
                }
            }
        }

        public void AddBrand(Brand brand)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Brand ([Name])
                    OUTPUT INSERTED.Id
                    VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", brand.Name);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    brand.Id = newlyCreatedId;
                }
            }
        }
    }
}
