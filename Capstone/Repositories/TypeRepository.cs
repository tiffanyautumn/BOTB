using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class TypeRepository : BaseRepository, ITypeRepository
    {
        public TypeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Type> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Type, Id
                        From Type";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var types = new List<Type>();
                        while (reader.Read())
                        {
                            types.Add(new Type()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Type"),
                            });
                        }
                        reader.Close();
                        return types;
                    }
                }
            }
        }

        public void AddType(Type type)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Type ([Type])
                    OUTPUT INSERTED.Id
                    VALUES (@type)";
                    DbUtils.AddParameter(cmd, "@type", type.Name);
                   
                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    type.Id = newlyCreatedId;
                }
            }
        }

    }
}
