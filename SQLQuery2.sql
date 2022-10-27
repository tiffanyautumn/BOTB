SELECT p.Id, p.Name, p.BrandId, p.TypeId, p.Price, p.ImageUrl, t.Name, 
                              pi.Id as PIId, pi.IngredientId, pi.ActiveIngredient, pi.[Order],
                              i.Name as IName,
                              piu.Id AS PIUId,
                              u.Description, u.Id AS UseId,
                              ir.Id AS ReviewId, ir.RateId, r.Rating
                       FROM Product p
                       LEFT JOIN Type t ON p.TypeId = t.Id
                       LEFT JOIN ProductIngredient pi ON pi.ProductId = p.Id
                       LEFT JOIN ProductIngredientUse piu ON pi.Id = piu.ProductIngredientId
                       LEFT JOIN [Use] u ON piu.UseId = u.Id
                       LEFT JOIN Ingredient i ON i.Id = pi.IngredientId
                       LEFT JOIN IngredientReview ir ON ir.IngredientId = i.Id
                       LEFT JOIN Rate r ON r.Id = ir.RateId