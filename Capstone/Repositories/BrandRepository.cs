using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Capstone.Models;
using Capstone.Repositories.Interfaces;

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

        public Brand GetBrandById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT b.Name, b.Id
                       FROM Brand b
                       WHERE b.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Brand brand = null;
                        while (reader.Read())
                        {
                            if (brand == null)
                            {
                                brand = new Brand()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name")
                                };

                            }
                        }
                        return brand;

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

        public void DeleteBrand(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Brand
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBrand(Brand brand)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Brand
                            SET 
                                [Name] = @name
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", brand.Name);
                    DbUtils.AddParameter(cmd, "@id", brand.Id);


                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
