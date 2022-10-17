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
  [Function] nvarchar(255),
  [SafetyInfo] nvarchar(255)
)
GO

CREATE TABLE [Product] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Brand] nvarchar(255) NOT NULL,
  [TypeId] int NOT NULL,
  [Price] decimal NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [Type] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Type] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [ProductIngredient] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [IngredientId] int NOT NULL,
  [ProductId] int NOT NULL,
  [Active] bit NOT NULL,
  [Use] nvarchar(255)
)
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [RoleId] int NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [FireBaseId] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [UserProduct] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [ProductId] int NOT NULL
)
GO

CREATE TABLE [Role] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Role] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [IngredientReview] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [RateId] int NOT NULL,
  [UserId] int NOT NULL,
  [IngredientId] int NOT NULL,
  [Review] nvarchar(255) NOT NULL,
  [Source] nvarchar(255) NOT NULL,
  [DateReviewed] datetime NOT NULL
)
GO

CREATE TABLE [Rate] (
  [Id] int PRIMARY KEY NOT NULL IDENTITY(1, 1),
  [Rating] nvarchar(255) NOT NULL
)
GO

ALTER TABLE [ProductIngredient] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductIngredient] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserProduct] ADD FOREIGN KEY ([UserId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [UserProduct] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([RateId]) REFERENCES [Rate] ([Id])
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([IngredientId]) REFERENCES [Ingredient] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [IngredientReview] ADD FOREIGN KEY ([UserId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([TypeId]) REFERENCES [Type] ([Id])
GO

ALTER TABLE [UserProfile] ADD FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id])
GO
