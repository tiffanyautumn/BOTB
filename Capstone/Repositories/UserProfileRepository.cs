using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Capstone.Models;
using static Capstone.Repositories.UserProfileRepository;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            SELECT up.Id, up.FirstName,up.LastName, up.Email, up.RoleId, r.Role  
                               FROM UserProfile up
                               LEFT JOIN Role r ON r.Id = up.RoleId";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var users = new List<UserProfile>();
                        while (reader.Read())
                        {
                            users.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                RoleId = DbUtils.GetInt(reader, "RoleId"),
                                Role = new Role()
                                {
                                    Id = DbUtils.GetInt(reader, "RoleId"),
                                    Name = DbUtils.GetString(reader, "Role"),
                                }

                            });
                        }

                        return users;
                    }
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT up.Id, up.FirstName,up.LastName, up.Email, up.RoleId, r.Role  
                               FROM UserProfile up
                               LEFT JOIN Role r ON r.Id = up.RoleId
                    WHERE u.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile profile = null;
                        while (reader.Read())
                        {
                            if (profile == null)
                            {
                                profile = new UserProfile()
                                {
                                    Id = id,
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    RoleId = DbUtils.GetInt(reader, "RoleId"),
                                    Role = new Role()
                                    {
                                        Id = DbUtils.GetInt(reader, "RoleId"),
                                        Name = DbUtils.GetString(reader, "Role"),
                                    }
                                };
                            }


                        }
                        return profile;
                    }
                }
            }
        }

        public UserProfile GetByFirebaseId(string fBId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT up.Id, up.FirstName,up.LastName, up.Email, up.RoleId, r.Role  
                               FROM UserProfile up
                               LEFT JOIN Role r ON r.Id = up.RoleId
                    WHERE up.FireBaseId = @Id";
                    DbUtils.AddParameter(cmd, "@Id", fBId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile profile = null;
                        while (reader.Read())
                        {
                            if (profile == null)
                            {
                                profile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    FirebaseUserId = fBId,
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    RoleId = DbUtils.GetInt(reader, "RoleId"),
                                    Role = new Role()
                                    {
                                        Id = DbUtils.GetInt(reader, "RoleId"),
                                        Name = DbUtils.GetString(reader, "Role"),
                                    }
                                };
                            }

                        }
                        return profile;
                    }
                }
            }
        }

        public void Add(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (FirstName, LastName, Email, RoleId, FireBaseId)
                        OUTPUT INSERTED.ID
                        VALUES (@FirstName, @LastName, @Email, @RoleId, @FireBaseUserId)";

                    DbUtils.AddParameter(cmd, "@FirstName", profile.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", profile.LastName);
                    DbUtils.AddParameter(cmd, "@Email", profile.Email);
                    DbUtils.AddParameter(cmd, "@RoleId", profile.RoleId);
                    DbUtils.AddParameter(cmd, "@FireBaseUserId", profile.FirebaseUserId);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}

