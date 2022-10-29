using Capstone.Models;
using Capstone.Repositories.Interfaces;
using Capstone.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
                       SELECT p.Id, p.Name, p.BrandId, p.TypeId, p.Price, p.ImageUrl, t.Name AS TName, 
                              pi.Id as PIId, pi.IngredientId, pi.ActiveIngredient, 
                              b.Name AS BName,
                              i.Name as IName
                       FROM Product p
                       LEFT JOIN [Type] t ON p.TypeId = t.Id
                       LEFT JOIN ProductIngredient pi ON pi.ProductId = p.Id
                       LEFT JOIN Brand b ON b.Id = p.BrandId
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
                                    },
                                    ProductId = productId,
                                    ActiveIngredient = DbUtils.GetBoolean(reader, "ActiveIngredient"),

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
                       SELECT p.Id, p.Name, p.BrandId, p.TypeId, p.Price, p.ImageUrl, t.Name AS TName,
                              b.Name AS BName
                       FROM Product p
                       LEFT JOIN Type t ON p.TypeId = t.Id
                       LEFT JOIN Brand b ON b.Id = p.BrandId
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
                                BrandId = @brandId,
                                TypeId = @typeId,
                                Price = @price,
                                ImageUrl = @imageUrl
                            WHERE Id = @id";


                    DbUtils.AddParameter(cmd, "@name", product.Name);
                    DbUtils.AddParameter(cmd, "@id", product.Id);
                    DbUtils.AddParameter(cmd, "@brandId", product.BrandId);
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
                    INSERT INTO Product (Name, BrandId, TypeId, Price, ImageUrl)
                    OUTPUT INSERTED.Id
                    VALUES (@name, @brandId, @typeId, @price, @imageUrl)";
                    DbUtils.AddParameter(cmd, "@name", product.Name);
                    DbUtils.AddParameter(cmd, "@brandId", product.BrandId);
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
                    SELECT p.Id, p.[Name], p.BrandId, p.TypeId, p.Price, p.ImageUrl, b.[Name] AS BName
                    FROM Product p
                    LEFT JOIN Brand b ON p.BrandId = b.Id
                    WHERE p.[Name] LIKE @Criterion OR b.Name LIKE @Criterion";
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
                                BrandId = DbUtils.GetInt(reader, "BrandId"),
                                Brand = new Brand()
                                {
                                    Id = DbUtils.GetInt(reader, "BrandId"),
                                    Name = DbUtils.GetString(reader, "BName"),
                                },
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

        public void AddUserProduct(Product product, int id)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO UserProduct (UserProfileId, ProductId)
                    OUTPUT INSERTED.Id
                    VALUES (@userProfileId, @productId)";
                    DbUtils.AddParameter(cmd, "@userProfileId", id);
                    DbUtils.AddParameter(cmd, "@productId", product.Id);

                    int newlyCreatedId = (int)cmd.ExecuteScalar();
                }
            }
        }

        public Product GetUserProductById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT p.Id AS productId, p.Name, p.BrandId, p.TypeId, p.Price, p.ImageUrl, t.Name AS TName,
                              b.Name AS BName,
                              up.Id, 
                       FROM UserProduct up
                       LEFT JOIN Product p ON up.ProductId = p.Id
                       LEFT JOIN Type t ON p.TypeId = t.Id
                       LEFT JOIN Brand b ON b.Id = p.BrandId
                       
                       WHERE up.Id = @id";

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

                        }
                        return product;
                    }
                }
            }
        }

        public List<UserProduct> GetUserProductsByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT up.Id,
                              b.Name AS BName, b.Id AS BrandId,
                              p.Name, p.Price, p.ImageUrl, p.TypeId, p.Id AS ProductId,
                              t.Name AS TName, t.Id AS TypeId
                       FROM UserProduct up
                       LEFT JOIN Product p ON p.Id = up.ProductId
                       LEFT JOIN Brand b ON b.Id = p.BrandId
                       LEFT JOIN Type t on t.Id = p.TypeId
                       WHERE up.UserProfileId = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProducts = new List<UserProduct>();
                        while (reader.Read())
                        {
                            UserProduct userProduct = new UserProduct()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Product = new Product()
                                {
                                    Id = DbUtils.GetInt(reader, "ProductId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    BrandId = DbUtils.GetInt(reader, "BrandId"),
                                    Brand = new Brand()
                                    {
                                        Id = DbUtils.GetInt(reader, "BrandId"),
                                        Name = DbUtils.GetString(reader, "BName")
                                    },
                                    Price = DbUtils.GetDecimal(reader, "Price"),
                                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                    TypeId = DbUtils.GetInt(reader, "TypeId"),
                                    Type = new Type()
                                    {
                                        Id = DbUtils.GetInt(reader, "TypeId"),
                                        Name = DbUtils.GetString(reader, "TName"),
                                    },
                                }
                            };
                            userProducts.Add(userProduct);
                        }


                        return userProducts;
                    }
                        

                    }
                }
            }
        
        public void DeleteUserProduct(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserProduct
                                       WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private Product NewProductFromReader(SqlDataReader reader)
        {
            return new Product
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                BrandId = DbUtils.GetInt(reader, "BrandId"),
                Brand = new Brand()
                {
                    Id = DbUtils.GetInt(reader, "BrandId"),
                    Name = DbUtils.GetString(reader, "BName")
                },
                Price = DbUtils.GetDecimal(reader, "Price"),
                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                TypeId = DbUtils.GetInt(reader, "TypeId"),
                Type = new Type()
                {
                    Id = DbUtils.GetInt(reader, "TypeId"),
                    Name = DbUtils.GetString(reader, "TName"),
                },
                ProductIngredients = new List<ProductIngredient>()
            };
        }

       
    }
}

