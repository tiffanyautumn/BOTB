USE [BOTB];
GO

SET IDENTITY_INSERT [Role] ON
INSERT INTO [Role]
	([Id], [Role])
VALUES
	(1, 'Admin'),
	(2, 'Approved'),
	(3, 'User');
SET IDENTITY_INSERT [Role] OFF

SET IDENTITY_INSERT [UserProfile] ON
INSERT INTO [UserProfile]
	([Id],[FireBaseId], [FirstName], [LastName], [Email], [RoleId])
VALUES
	(1, 'UFbPNahn6zX5SKT9jKq8qUwK9632', 'Hazel', 'Ohde', 'h@h.com', 1),
	(2, 'HgapINvaKvQXs47HHtbWOLetP1J2', 'Boone', 'Ohde', 'b@b.com', 2),
	(3, 'fwgaE3R7lXQM29lYwjZxFDgUvZh1', 'Tiffany', 'Smith', 't@t.com', 3);
SET IDENTITY_INSERT [UserProfile] OFF

SET IDENTITY_INSERT [Type] ON
INSERT INTO [Type]
	([Id],[Type])
VALUES
	(1, 'Face Moisturizer'),
	(2, 'Deodorant'),
	(3, 'Face Serum');
SET IDENTITY_INSERT [Type] OFF

SET IDENTITY_INSERT [Product] ON
INSERT INTO [Product]
	([Id],[Name], [Brand], [TypeId], [Price], [ImageUrl])
VALUES
	(1, 'AM Facial Moisturizing Lotion', 'CeraVe', 1, 14.99 , null),
	(2, 'Advanced Care Antiperspirant Deodorant Stick', 'Dove', 2, 5.00, null),
	(3, 'Niacinamide 10% + Zinc 1%', 'The Ordinary', 3 , 11.70, 'https://theordinary.com/dw/image/v2/BFKJ_PRD/on/demandware.static/-/Sites-deciem-master/default/dw9329e382/Images/products/The%20Ordinary/rdn-niacinamide-10pct-zinc-1pct-60ml.png?sw=1200&sh=1200&sm=fit');
SET IDENTITY_INSERT [Product] OFF

SET IDENTITY_INSERT [Ingredient] ON
INSERT INTO [Ingredient]
	([Id],[Name], [Function],[SafetyInfo])
VALUES
	(1, 'Homosalate 10%', 'absorbs UV rays', 'safe for skin'),
	(2, 'Meradimate 5%', 'use', null),
	(3, 'Octinoxate 5%', null, null),
	(4, 'Aluminum Zirconium Tetrachlorohydrex GLY (15.2%)', 'antiperspirant', null ),
	(5, 'Stearyl Alcohol', null, null),
	(6, 'Niacinamide 10%', null, null),
	(7, 'Zinc PCA 1%', null, null),
	(8, 'Aqua (Water)', null, 'safe');
SET IDENTITY_INSERT [Ingredient] OFF

SET IDENTITY_INSERT [ProductIngredient] ON
INSERT INTO [ProductIngredient]
	([Id],[IngredientId], [ProductId],[Active],[Use])
VALUES
	(1, 1 , 1, 1, 'absorbs UV rays'),
	(2, 2, 1, 1, null),
	(3, 3, 1, 1, null),
	(4, 4, 2, 1, 'antiperspirant' ),
	(5, 5, 2, 0, null),
	(6, 6, 3, 1, null),
	(7, 7, 3, 1, null),
	(8, 8, 3, 0, null);
SET IDENTITY_INSERT [ProductIngredient] OFF

SET IDENTITY_INSERT [Rate] ON
INSERT INTO [Rate]
	([Id],[Rating])
VALUES
	(1, 'Safe for Skin');
SET IDENTITY_INSERT [Rate] OFF

SET IDENTITY_INSERT [IngredientReview] ON
INSERT INTO [IngredientReview]
	([Id],[RateId], [UserId], [IngredientId], [Review], [Source], [DateReviewed])
VALUES
	(1, 1, 2, 1, 'Homosalate is a safe ingredient that can be used for sun protection. However, it should be kept away from the eyes.', 'https://pubchem.ncbi.nlm.nih.gov/compound/Homosalate', '2022-10-17' );
SET IDENTITY_INSERT [IngredientReview] OFF

SET IDENTITY_INSERT [UserProduct] ON
INSERT INTO [UserProduct]
	([Id],[UserId], [ProductId])
VALUES
	(1, 3, 1);
SET IDENTITY_INSERT [UserProduct] OFF