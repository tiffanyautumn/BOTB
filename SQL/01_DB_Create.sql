USE [master]
GO

IF db_id('BOTB') IS NOT NULL
BEGIN
  ALTER DATABASE [BOTB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [BOTB]
END
GO

CREATE DATABASE [BOTB]
GO

USE [BOTB]
GO

---------------------------------------------------------------------------

CREATE TABLE [Ingredient] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [IngredientReview] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [IngredientId] int NOT NULL,
  [Review] nvarchar(255) NOT NULL,
  [UserProfileId] int NOT NULL,
  [RateId] int NOT NULL,
  [DateReviewed] datetime NOT NULL
)
GO

CREATE TABLE [Source] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Link] nvarchar(255) NOT NULL,
  [IngredientReviewId] int NOT NULL
)
GO

CREATE TABLE [Product] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [BrandId] int NOT NULL,
  [TypeId] int NOT NULL,
  [Price] decimal NOT NULL,
  [ImageUrl] nvarchar(255) 
)
GO

CREATE TABLE [Type] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [ProductIngredient] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [IngredientId] int NOT NULL,
  [ProductId] int NOT NULL,
  [ActiveIngredient] bit NOT NULL,
  [Order] int NOT NULL
)
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserTypeId] int NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [FireBaseId] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserProduct] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [ProductId] int NOT NULL
)
GO

CREATE TABLE [UserType] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Type] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Brand] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Rate] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Rating] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Use] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Description] nvarchar(255) NOT NULL,
  [IngredientId] int NOT NULL
)
GO

CREATE TABLE [ProductIngredientUse] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UseId] int NOT NULL,
  [ProductIngredientId] int NOT NULL
)
GO

CREATE TABLE [IngredientHazard] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [IngredientId] int NOT NULL,
  [HazardId] int NOT NULL,
  [Case] nvarchar(255)
)
GO

CREATE TABLE [Hazard] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255)
)
GO

CREATE TABLE [ProductReview] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [ProductId] int NOT NULL,
  [Comment] nvarchar(255),
  [AffordabilityRate] int NOT NULL,
  [EfficacyRate] int NOT NULL,
  [OverallRate] int NOT NULL
)
GO

ALTER TABLE [UserProfile] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([BrandId]) REFERENCES [Brand] ([Id])
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([TypeId]) REFERENCES [Type] ([Id])
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductIngredient] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductIngredient] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductIngredientUse] ADD FOREIGN KEY ([ProductIngredientId]) REFERENCES [ProductIngredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Use] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductIngredientUse] ADD FOREIGN KEY ([UseId]) REFERENCES [Use] ([Id])
GO

ALTER TABLE [IngredientHazard] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [IngredientHazard] ADD FOREIGN KEY ([HazardId]) REFERENCES [Hazard] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([RateId]) REFERENCES [Rate] ([Id])
GO

ALTER TABLE [UserProduct] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserProduct] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductReview] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductReview] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Source] ADD FOREIGN KEY ([IngredientReviewId]) REFERENCES [IngredientReview] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO
