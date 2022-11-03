USE [BOTB];
GO

SET IDENTITY_INSERT [UserType] ON
INSERT INTO [UserType]
	([Id], [Type])
VALUES
	(1, 'Admin'),
	(2, 'Approved'),
	(3, 'User');
SET IDENTITY_INSERT [UserType] OFF

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile]
	([Id],[FireBaseId], [FirstName], [LastName], [Email], [UserTypeId])
VALUES
	(1, 'UFbPNahn6zX5SKT9jKq8qUwK9632', 'Hazel', 'Ohde', 'h@h.com', 1),
	(2, 'HgapINvaKvQXs47HHtbWOLetP1J2', 'Boone', 'Ohde', 'b@b.com', 2),
	(3, 'fwgaE3R7lXQM29lYwjZxFDgUvZh1', 'Tiffany', 'Smith', 't@t.com', 3);
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Type] ON
INSERT INTO [Type]
	([Id],[Name])
VALUES
	(1, 'Face Moisturizer'),
	(2, 'Deodorant'),
	(3, 'Face Serum');
SET IDENTITY_INSERT [Type] OFF

SET IDENTITY_INSERT [Brand] ON
INSERT INTO [Brand]
	([Id],[Name])
VALUES
	(1, 'CeraVe'),
	(2, 'Dove'),
	(3, 'The Ordinary');
SET IDENTITY_INSERT [Brand] OFF

SET IDENTITY_INSERT [Product] ON
INSERT INTO [Product]
	([Id],[Name], [BrandId], [TypeId], [Price], [ImageUrl])
VALUES
	(1, 'AM Facial Moisturizing Lotion', 1, 1, 14.99 , null),
	(2, 'Advanced Care Antiperspirant Deodorant Stick', 2, 2, 5.00, null),
	(3, 'Niacinamide 10% + Zinc 1%', 3, 3 , 11.70, 'https://theordinary.com/dw/image/v2/BFKJ_PRD/on/demandware.static/-/Sites-deciem-master/default/dw9329e382/Images/products/The%20Ordinary/rdn-niacinamide-10pct-zinc-1pct-60ml.png?sw=1200&sh=1200&sm=fit');
SET IDENTITY_INSERT [Product] OFF

SET IDENTITY_INSERT [Ingredient] ON
INSERT INTO [Ingredient]
	([Id],[Name], [ImageUrl])
VALUES
	(1, 'Homosalate 10%', null),
	(2, 'Meradimate 5%', null),
	(3, 'Octinoxate 5%', null),
	(4, 'Aluminum Zirconium Tetrachlorohydrex GLY (15.2%)', null ),
	(5, 'Stearyl Alcohol', null),
	(6, 'Niacinamide 10%', null),
	(7, 'Zinc PCA 1%', null),
	(8, 'Aqua (Water)', null);
SET IDENTITY_INSERT [Ingredient] OFF

SET IDENTITY_INSERT [ProductIngredient] ON
INSERT INTO [ProductIngredient]
	([Id],[IngredientId], [ProductId],[ActiveIngredient],[Order])
VALUES
	(1, 1 , 1, 1, 1),
	(2, 2, 1, 1, 2),
	(3, 3, 1, 1, 3),
	(4, 4, 2, 1, 1 ),
	(5, 5, 2, 0, 2),
	(6, 6, 3, 1, 1),
	(7, 7, 3, 1, 2),
	(8, 8, 3, 0, 3);
SET IDENTITY_INSERT [ProductIngredient] OFF

SET IDENTITY_INSERT [Rate] ON
INSERT INTO [Rate]
	([Id],[Rating])
VALUES
	(1, 'Safe for Skin');
SET IDENTITY_INSERT [Rate] OFF

SET IDENTITY_INSERT [IngredientReview] ON
INSERT INTO [IngredientReview]
	([Id],[RateId], [UserProfileId], [IngredientId], [Review], [DateReviewed])
VALUES
	(1, 1, 2, 1, 'Homosalate is a safe ingredient that can be used for sun protection. However, it should be kept away from the eyes.',  '2022-10-17' );
SET IDENTITY_INSERT [IngredientReview] OFF

SET IDENTITY_INSERT [Source] ON
INSERT INTO [Source]
	([Id],[Link], [IngredientReviewId])
VALUES
	(1, 'https://pubchem.ncbi.nlm.nih.gov/compound/Homosalate', 1);
SET IDENTITY_INSERT [Source] OFF

SET IDENTITY_INSERT [UserProduct] ON
INSERT INTO [UserProduct]
	([Id],[UserProfileId], [ProductId])
VALUES
	(1, 3, 1);
SET IDENTITY_INSERT [UserProduct] OFF

SET IDENTITY_INSERT [Use] ON
INSERT INTO [Use]
	([Id],[Description], [IngredientId])
VALUES
	(1, 'UV protection', 1);
SET IDENTITY_INSERT [Use] OFF

SET IDENTITY_INSERT [ProductIngredientUse] ON
INSERT INTO [ProductIngredientUse]
	([Id],[UseId], [ProductIngredientId])
VALUES
	(1, 1, 1);
SET IDENTITY_INSERT [ProductIngredientUse] OFF

SET IDENTITY_INSERT [Hazard] ON
INSERT INTO [Hazard]
	([Id],[Name], [Description])
VALUES
	(1, 'Irritant', 'May cause skin irritation');
SET IDENTITY_INSERT [Hazard] OFF

SET IDENTITY_INSERT [IngredientHazard] ON
INSERT INTO [IngredientHazard]
	([Id],[IngredientId], [HazardId],[Case])
VALUES
	(1, 1, 1, 'Only at high concentrations');
SET IDENTITY_INSERT [IngredientHazard] OFF

SET IDENTITY_INSERT [ProductReview] ON
INSERT INTO [ProductReview]
	([Id], [UserProfileId], [ProductId], [Comment], [AffordabilityRate], [EfficacyRate], [OverallRate])
VALUES
	(1, 3, 1, 'this is a great product', 7, 8, 8);
SET IDENTITY_INSERT [ProductReview] OFF


