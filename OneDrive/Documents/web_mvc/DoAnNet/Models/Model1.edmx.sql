
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/11/2023 18:40:08
-- Generated from EDMX file: C:\Users\acer\Documents\Do An Web Moi Nhat\DoAn\DoAnNet\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SmartphoneShop];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Orders_OrderId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Orders_OrderId];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetails_Products_ProductId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetails] DROP CONSTRAINT [FK_OrderDetails_Products_ProductId];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Categories_CategoryId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Categories_CategoryId];
GO
IF OBJECT_ID(N'[dbo].[FK_Products_Vendors_VendorId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_Products_Vendors_VendorId];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCartItems_Products_ProductId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCartItems] DROP CONSTRAINT [FK_ShoppingCartItems_Products_ProductId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__EFMigrationsHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__EFMigrationsHistory];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[OrderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetails];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCartItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCartItems];
GO
IF OBJECT_ID(N'[dbo].[TaiKhoan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaiKhoan];
GO
IF OBJECT_ID(N'[dbo].[Vendors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vendors];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__EFMigrationsHistory'
CREATE TABLE [dbo].[C__EFMigrationsHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [Amount] int  NOT NULL,
    [Price] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [OrderTotal] decimal(18,2)  NOT NULL,
    [OrderPlaced] datetime  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Price] decimal(18,2)  NOT NULL,
    [Quantity] int  NOT NULL,
    [IsLatestProduct] bit  NOT NULL,
    [IsTrendingProduct] bit  NOT NULL,
    [IsSpecialProduct] bit  NOT NULL,
    [Guarantee] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [VendorId] int  NOT NULL,
    [DiscountPercent] int  NULL,
    [Img] char(255)  NULL
);
GO

-- Creating table 'ShoppingCartItems'
CREATE TABLE [dbo].[ShoppingCartItems] (
    [ShoppingCartItemId] int IDENTITY(1,1) NOT NULL,
    [ProductId] int  NOT NULL,
    [Amount] int  NOT NULL,
    [ShoppingCartId] nvarchar(max)  NULL
);
GO

-- Creating table 'TaiKhoans'
CREATE TABLE [dbo].[TaiKhoans] (
    [TenTaiKhoan] varchar(50)  NOT NULL,
    [MatKhau] varchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [Quyen] varchar(10)  NULL,
    [TenHienThi] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Vendors'
CREATE TABLE [dbo].[Vendors] (
    [VendorId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId] in table 'C__EFMigrationsHistory'
ALTER TABLE [dbo].[C__EFMigrationsHistory]
ADD CONSTRAINT [PK_C__EFMigrationsHistory]
    PRIMARY KEY CLUSTERED ([MigrationId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [OrderDetailId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([OrderDetailId] ASC);
GO

-- Creating primary key on [OrderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [ShoppingCartItemId] in table 'ShoppingCartItems'
ALTER TABLE [dbo].[ShoppingCartItems]
ADD CONSTRAINT [PK_ShoppingCartItems]
    PRIMARY KEY CLUSTERED ([ShoppingCartItemId] ASC);
GO

-- Creating primary key on [TenTaiKhoan] in table 'TaiKhoans'
ALTER TABLE [dbo].[TaiKhoans]
ADD CONSTRAINT [PK_TaiKhoans]
    PRIMARY KEY CLUSTERED ([TenTaiKhoan] ASC);
GO

-- Creating primary key on [VendorId] in table 'Vendors'
ALTER TABLE [dbo].[Vendors]
ADD CONSTRAINT [PK_Vendors]
    PRIMARY KEY CLUSTERED ([VendorId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Categories_CategoryId]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Categories_CategoryId'
CREATE INDEX [IX_FK_Products_Categories_CategoryId]
ON [dbo].[Products]
    ([CategoryId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Orders_OrderId]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Orders_OrderId'
CREATE INDEX [IX_FK_OrderDetails_Orders_OrderId]
ON [dbo].[OrderDetails]
    ([OrderId]);
GO

-- Creating foreign key on [ProductId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetails_Products_ProductId]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetails_Products_ProductId'
CREATE INDEX [IX_FK_OrderDetails_Products_ProductId]
ON [dbo].[OrderDetails]
    ([ProductId]);
GO

-- Creating foreign key on [VendorId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Vendors_VendorId]
    FOREIGN KEY ([VendorId])
    REFERENCES [dbo].[Vendors]
        ([VendorId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Products_Vendors_VendorId'
CREATE INDEX [IX_FK_Products_Vendors_VendorId]
ON [dbo].[Products]
    ([VendorId]);
GO

-- Creating foreign key on [ProductId] in table 'ShoppingCartItems'
ALTER TABLE [dbo].[ShoppingCartItems]
ADD CONSTRAINT [FK_ShoppingCartItems_Products_ProductId]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([ProductId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCartItems_Products_ProductId'
CREATE INDEX [IX_FK_ShoppingCartItems_Products_ProductId]
ON [dbo].[ShoppingCartItems]
    ([ProductId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------