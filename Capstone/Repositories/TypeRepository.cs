using Capstone.Models;
using Capstone.Repositories.Interfaces;
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
                        SELECT Name, Id
                        From [Type]";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var types = new List<Type>();
                        while (reader.Read())
                        {
                            types.Add(new Type()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            });
                        }
                        reader.Close();
                        return types;
                    }
                }
            }
        }

        public Type GetTypeById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT t.Name, t.Id
                       FROM Type t
                       WHERE t.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Type type = null;
                        while (reader.Read())
                        {
                            if (type == null)
                            {
                                type = new Type()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name")
                                };
                                
                            }
                        }
                        return type;

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
                    INSERT INTO Type ([Name])
                    OUTPUT INSERTED.Id
                    VALUES (@name)";
                    DbUtils.AddParameter(cmd, "@name", type.Name);
                   
                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    type.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteType(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Type
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateType(Type type)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Type
                            SET 
                                [Name] = @name
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", type.Name);
                    DbUtils.AddParameter(cmd, "@id", type.Id);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }

       

    }
}
