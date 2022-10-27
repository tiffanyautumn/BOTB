using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

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
                       SELECT pi.Id, pi.ProductId, pi.ActiveIngredient, pi.[Order], pi.IngredientId,
                              i.Name AS IngredientName, 
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

        public List<ProductIngredient> GetProductIngredientsByProductId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT pi.Id, pi.ProductId, pi.ActiveIngredient, pi.[Order], pi.IngredientId,
                              piu.Id AS PIUId, piu.UseId,
                              u.Description, u.Id AS UseId,
                              i.Name AS IngredientName, 
                              p.Name AS ProductName
                       FROM ProductIngredient pi
                       LEFT JOIN Ingredient i ON pi.IngredientId = i.Id
                       LEFT JOIN ProductIngredientUse piu ON piu.ProductIngredientId = pi.Id
                       LEFT JOIN [Use] u ON piu.UseId = u.Id
                       LEFT JOIN Product p ON p.Id = pi.ProductId
                       WHERE p.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var productIngredients = new List<ProductIngredient>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ProductIngredient productIngredient = null;
                        while (reader.Read())
                        {
                            var productIngredientId = DbUtils.GetInt(reader, "Id");
                            var existingProductIngredient = productIngredients.FirstOrDefault(p => p.Id == productIngredientId);
                            if (productIngredient == null)
                            {
                                existingProductIngredient = new ProductIngredient()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    ProductId = DbUtils.GetInt(reader, "ProductId"),
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "IngredientName"),
                                    },
                                    Product = new Product()
                                    {
                                        Id = DbUtils.GetInt(reader, "ProductId"),
                                        Name = DbUtils.GetString(reader, "ProductName"),
                                    },
                                    Uses = new List<Use>()
                                };
                                productIngredients.Add(existingProductIngredient);
                            }
                            if (DbUtils.IsNotDbNull(reader, "PIUId"))
                            {
                            productIngredient.Uses.Add(new Use()
                            {
                                Id = DbUtils.GetInt(reader, "UseId"),
                                Description = DbUtils.GetString(reader, "Description"),
                            });
                            }
                           
                        }
                        return productIngredients;

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
                                [ActiveIngredient] = @activeIngredient,
                                [Order] = @order
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@ingredientId", productIngredient.IngredientId);
                    DbUtils.AddParameter(cmd, "@id", productIngredient.Id);
                    DbUtils.AddParameter(cmd, "@activeIngredient", productIngredient.ActiveIngredient);
                    DbUtils.AddParameter(cmd, "@order", productIngredient.Order);
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
                    INSERT INTO ProductIngredient (IngredientId, ProductId, ActiveIngredient, [Order])
                    OUTPUT INSERTED.Id
                    VALUES (@ingredientId, @productId, @activeIngredient, @order)";
                    DbUtils.AddParameter(cmd, "@ingredientId", productIngredient.IngredientId);
                    DbUtils.AddParameter(cmd, "@order", productIngredient.Order);
                    DbUtils.AddParameter(cmd, "@activeIngredient", productIngredient.ActiveIngredient);
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
