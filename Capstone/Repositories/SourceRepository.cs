using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class SourceRepository : BaseRepository, ISourceRepository
    {
        public SourceRepository(IConfiguration configuration) : base(configuration){ }

        public List<Source> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Link, IngredientReviewId, Id
                       FROM Source";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var sources = new List<Source>();

                        while (reader.Read())
                        {
                            sources.Add(new Source()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Link = DbUtils.GetString(reader, "Link"),
                                IngredientReviewId = DbUtils.GetInt(reader, "IngredientReviewId")
                            });


                        };
                        reader.Close();
                        return sources;

                    }


                }
            }
        }

        public Source GetSourceById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT s.[Link], s.Id, s.IngredientReviewId
                       FROM Source s
                       WHERE s.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Source source = null;
                        while (reader.Read())
                        {
                            if (source == null)
                            {
                                source = new Source()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Link = DbUtils.GetString(reader, "Link"),
                                    IngredientReviewId = DbUtils.GetInt(reader, "IngredientReviewId")

                                };

                            }
                        }
                        return source;

                    }
                }
            }
        }
        public List<Source> GetAllByReviewId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT s.Id AS SourceId, s.Link, s.IngredientReviewId, ir.Id
                       FROM Source s
                       LEFT JOIN IngredientReview ir ON ir.Id = s.IngredientReviewId
                       WHERE ir.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var sources = new List<Source>();

                        while (reader.Read())
                        {
                            sources.Add(new Source()
                            {
                                Id = DbUtils.GetInt(reader, "SourceId"),
                                Link = DbUtils.GetString(reader, "Link"),
                                IngredientReviewId = DbUtils.GetInt(reader, "IngredientReviewId")
                            });


                        };
                        reader.Close();
                        return sources;

                    }


                }
            }
        }

        public void AddSource(Source source)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Source] ([Link], IngredientReviewId)
                    OUTPUT INSERTED.Id
                    VALUES (@link, @ingredientReviewId)";
                    DbUtils.AddParameter(cmd, "@link", source.Link);
                    DbUtils.AddParameter(cmd, "@ingredientReviewId", source.IngredientReviewId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    source.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteSource(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [Source]
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSource(Source source)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [Source]
                            SET 
                                [Link] = @link,
                                IngredientReviewId = @ingredientReviewId
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@link", source.Link);
                    DbUtils.AddParameter(cmd, "@ingredientReviewId", source.IngredientReviewId);



                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
