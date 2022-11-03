SELECT pi.Id, pi.ProductId, pi.ActiveIngredient, pi.[Order], pi.IngredientId,
                              piu.Id AS PIUId, piu.UseId,
                              u.Description, 
                              i.Name AS IngredientName, 
                              p.Name AS ProductName
                       FROM ProductIngredient pi
                       LEFT JOIN Ingredient i ON pi.IngredientId = i.Id
                       LEFT JOIN ProductIngredientUse piu ON piu.ProductIngredientId = pi.Id
                       LEFT JOIN [Use] u ON piu.UseId = u.Id
                       LEFT JOIN Product p ON p.Id = pi.ProductId
                       WHERE p.Id = 1