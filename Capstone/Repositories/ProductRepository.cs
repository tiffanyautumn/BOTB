using Capstone.Models;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration) { }

        public List<Product> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT p.Id, p.Name, p.Brand, p.TypeId, p.Price, p.ImageUrl, t.Type, 
                              pi.Id as PIId, pi.IngredientId, pi.Active, pi.[Use],
                              i.Name as IName, i.SafetyInfo
                       FROM Product p
                       LEFT JOIN Type t ON p.TypeId = t.Id
                       LEFT JOIN ProductIngredient pi ON pi.ProductId = p.Id
                       LEFT JOIN Ingredient i ON i.Id = pi.IngredientId
                       ORDER BY p.Name";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var products = new List<Product>();

                        while (reader.Read())
                        {
                            var productId = DbUtils.GetInt(reader, "Id");

                            var existingProduct = products.FirstOrDefault(p => p.Id == productId);

                            if (existingProduct == null)
                            {
                                existingProduct = (NewProductFromReader(reader));
                                products.Add(existingProduct);
                            };
                            if (DbUtils.IsNotDbNull(reader, "PIId"))
                            {
                                existingProduct.ProductIngredients.Add(new ProductIngredient()
                                {
                                    Id = DbUtils.GetInt(reader, "PIId"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "IName"),
                                        SafetyInfo = DbUtils.GetString(reader, "SafetyInfo")
                                    },
                                    ProductId = productId,
                                    Active = DbUtils.GetBoolean(reader, "Active"),
                                    Use = DbUtils.GetString(reader, "Use")

                                });
                            }

                        }
                        reader.Close();
                        return products;

                    }
                }
            }
        }

        public Product GetProductById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT p.Id, p.Name, p.Brand, p.TypeId, p.Price, p.ImageUrl, t.Type, 
                              pi.Id as PIId, pi.IngredientId, pi.Active, pi.[Use],
                              i.Name as IName, i.SafetyInfo,
                              ir.Id AS ReviewId, ir.RateId, r.Rating
                       FROM Product p
                       LEFT JOIN Type t ON p.TypeId = t.Id
                       LEFT JOIN ProductIngredient pi ON pi.ProductId = p.Id
                       LEFT JOIN Ingredient i ON i.Id = pi.IngredientId
                       LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                       LEFT JOIN Rate r ON r.Id = ir.RateId
                       WHERE p.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Product product = null;
                        while (reader.Read())
                        {
                            if (product == null)
                            {
                                product = (NewProductFromReader(reader));
                            }
                            if (DbUtils.IsNotDbNull(reader, "PIId"))
                            {
                                ProductIngredient productIngredient = null;
                                product.ProductIngredients.Add( productIngredient = new ProductIngredient()
                                {
                                    Id = DbUtils.GetInt(reader, "PIId"),
                                    IngredientId = DbUtils.GetInt(reader, "IngredientId"),
                                    Ingredient = new Ingredient()
                                    {
                                        Id = DbUtils.GetInt(reader, "IngredientId"),
                                        Name = DbUtils.GetString(reader, "IName"),
                                        SafetyInfo = DbUtils.GetString(reader, "SafetyInfo")
                                    },
                                    ProductId = id,
                                    Active = DbUtils.GetBoolean(reader, "Active"),
                                    Use = DbUtils.GetString(reader, "Use"),
                                    

                                });
                            if(DbUtils.IsNotDbNull(reader, "ReviewId"))
                            {
                                    productIngredient.Ingredient.IngredientReview = new IngredientReview()
                                    {
                                        Id = DbUtils.GetInt(reader, "ReviewId"),
                                        Rate = new Rate()
                                        {
                                            Id = DbUtils.GetInt(reader, "RateId"),
                                            Rating = DbUtils.GetString(reader, "Rating")
                                        }

                                    };
                            }
                            }
                        }
                            

                            return product;
                    }
                        
                }
            }
        }


        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Product
                            SET 
                                [Name] = @name,
                                Brand = @brand,
                                TypeId = @typeId,
                                Price = @price,
                                ImageUrl = @imageUrl
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", product.Name);
                    DbUtils.AddParameter(cmd, "@id", product.Id);
                    DbUtils.AddParameter(cmd, "@brand", product.Brand);
                    DbUtils.AddParameter(cmd, "@typeId", product.TypeId);
                    DbUtils.AddParameter(cmd, "@price", product.Price);
                    DbUtils.AddParameter(cmd, "@imageUrl", product.ImageUrl);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Product (Name, Brand, TypeId, Price, ImageUrl)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @brand, @typeId, @price, @imageUrl)";
                    DbUtils.AddParameter(cmd, "@name", product.Name);
                    DbUtils.AddParameter(cmd, "@brand", product.Brand);
                    DbUtils.AddParameter(cmd, "@typeId", product.TypeId);
                    DbUtils.AddParameter(cmd, "@price", product.Price);
                    DbUtils.AddParameter(cmd, "@imageUrl", product.ImageUrl);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                    product.Id = newlyCreatedId;
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Product
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Product> Search(string criterion)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    var sql = @"
                    SELECT p.Id, p.[Name], p.Brand, p.TypeId, p.Price, p.ImageUrl
                    FROM Product p
                    WHERE p.[Name] LIKE @Criterion OR p.Brand LIKE @Criterion";
                    cmd.CommandText = sql;
                    DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var products = new List<Product>();
                        while(reader.Read())
                        {
                            products.Add(new Product()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Brand = DbUtils.GetString(reader, "Brand"),
                                Price = DbUtils.GetDecimal(reader, "Price"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                TypeId = DbUtils.GetInt(reader, "TypeId"),
                            });
                        }
                        return products;
                    }
                }
            }
        }


        private Product NewProductFromReader(SqlDataReader reader)
        {
            return new Product
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                Brand = DbUtils.GetString(reader, "Brand"),
                Price = DbUtils.GetDecimal(reader, "Price"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                TypeId = DbUtils.GetInt(reader, "TypeId"),
                Type = new Type()
                {
                    Id = DbUtils.GetInt(reader, "TypeId"),
                    Name = DbUtils.GetString(reader, "Type"),
                },
                ProductIngredients = new List<ProductIngredient>()
            };
        }
    }
}

