using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Capstone.Repositories
{
    public class ProductIngredientRepository : BaseRepository, IProductIngredientRepository
    {
        public ProductIngredientRepository(IConfiguration configuration) : base(configuration) { }



        public ProductIngredient GetProductIngredientById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT pi.Id, pi.ProductId, pi.Active, pi.[Use], pi.IngredientId,
                              i.Name AS IngredientName, i.[Function], i.SafetyInfo,
                              p.Name AS ProductName
                       FROM ProductIngredient pi
                       LEFT JOIN Ingredient i ON pi.IngredientId = i.Id
                       LEFT JOIN Product p ON p.Id = pi.ProductId
                       WHERE pi.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ProductIngredient productIngredient = null;
                        while (reader.Read())
                        {
                            if (productIngredient == null)
                            {
                                productIngredient = new ProductIngredient()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    ProductId = DbUtils.GetInt(reader, "ProductId"),
                                    Active = DbUtils.GetBoolean(reader, "Active"),
                                    Use = DbUtils.GetString(reader, "Use"),
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "IngredientName"),
                                    },
                                    Product = new Product()
                                    {
                                        Id = DbUtils.GetInt(reader, "ProductId"),
                                        Name = DbUtils.GetString(reader, "ProductName"),
                                    }

                                };

                            }
                        }
                        return productIngredient;

                    }
                }
            }
        }


        public void UpdateProductIngredient(ProductIngredient productIngredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE ProductIngredient
                            SET 
                                IngredientId = @ingredientId,
                                ProductId = @productId,
                                [Active] = @active,
                                [Use] = @use
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@ingredientId", productIngredient.IngredientId);
                    DbUtils.AddParameter(cmd, "@id", productIngredient.Id);
                    DbUtils.AddParameter(cmd, "@active", productIngredient.Active);
                    DbUtils.AddParameter(cmd, "@use", productIngredient.Use);
                    DbUtils.AddParameter(cmd, "@productId", productIngredient.ProductId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddProductIngredient(ProductIngredient productIngredient)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO ProductIngredient (IngredientId, ProductId, Active, [Use])
                    OUTPUT INSERTED.Id
                    VALUES (@ingredientId, @productId, @active, @use)";
                    DbUtils.AddParameter(cmd, "@ingredientId", productIngredient.IngredientId);
                    DbUtils.AddParameter(cmd, "@active", productIngredient.Active);
                    DbUtils.AddParameter(cmd, "@use", productIngredient.Use);
                    DbUtils.AddParameter(cmd, "@productId", productIngredient.ProductId);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    productIngredient.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteProductIngredient(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM ProductIngredient
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
