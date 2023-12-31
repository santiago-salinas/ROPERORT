USE [master]
GO
/****** Object:  Database [ECommerceDB]    Script Date: 16/11/2023 15:17:00 ******/
CREATE DATABASE [ECommerceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ECommerceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS2022\MSSQL\DATA\ECommerceDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ECommerceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS2022\MSSQL\DATA\ECommerceDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ECommerceDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ECommerceDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ECommerceDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ECommerceDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ECommerceDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ECommerceDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ECommerceDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ECommerceDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ECommerceDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ECommerceDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ECommerceDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ECommerceDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ECommerceDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ECommerceDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ECommerceDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ECommerceDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ECommerceDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ECommerceDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ECommerceDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ECommerceDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ECommerceDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ECommerceDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ECommerceDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ECommerceDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ECommerceDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ECommerceDB] SET  MULTI_USER 
GO
ALTER DATABASE [ECommerceDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ECommerceDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ECommerceDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ECommerceDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ECommerceDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ECommerceDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ECommerceDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ECommerceDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ECommerceDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignedRoles]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignedRoles](
	[UserId] [int] NOT NULL,
	[RoleName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AssignedRoles] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandEntities](
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_BrandEntities] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryEntities](
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_CategoryEntities] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ColourEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColourEntities](
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ColourEntities] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [nvarchar](450) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Bank] [nvarchar](max) NOT NULL,
	[Company] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductColors]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductColors](
	[ProductId] [int] NOT NULL,
	[ColourName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ProductColors] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[ColourName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Exclude] [bit] NOT NULL,
	[BrandName] [nvarchar](450) NULL,
	[CategoryName] [nvarchar](450) NULL,
	[Stock] [int] NOT NULL,
 CONSTRAINT [PK_ProductEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchasedProductEntity]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasedProductEntity](
	[PurchaseId] [int] NOT NULL,
	[ProductName] [nvarchar](450) NOT NULL,
	[Amount] [int] NOT NULL,
	[ProductPrice] [float] NOT NULL,
 CONSTRAINT [PK_PurchasedProductEntity] PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC,
	[ProductName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppliedPromotion] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[FinalPrice] [float] NOT NULL,
	[MoneyDiscounted] [float] NOT NULL,
	[PaymentMethodId] [nvarchar](450) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleEntities](
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_RoleEntities] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEntities]    Script Date: 16/11/2023 15:17:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231112203222_purchasev2', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231112205546_v3', N'6.0.21')
GO
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (1, N'Customer')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (2, N'Customer')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (3, N'Customer')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (4, N'Customer')
INSERT [dbo].[AssignedRoles] ([UserId], [RoleName]) VALUES (5, N'Admin')
GO
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'Addidas')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'DC')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'Nike')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'ORT')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'Puma')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'Renner')
INSERT [dbo].[BrandEntities] ([Name]) VALUES (N'Zara')
GO
INSERT [dbo].[CategoryEntities] ([Name]) VALUES (N'Accesorios')
INSERT [dbo].[CategoryEntities] ([Name]) VALUES (N'Camisa')
INSERT [dbo].[CategoryEntities] ([Name]) VALUES (N'Mascotas')
INSERT [dbo].[CategoryEntities] ([Name]) VALUES (N'Pantalones')
INSERT [dbo].[CategoryEntities] ([Name]) VALUES (N'Shorts')
GO
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Amarillo')
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Azul')
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Blanco')
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Negro')
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Rojo')
INSERT [dbo].[ColourEntities] ([Name]) VALUES (N'Verde')
GO
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'18292', N'CreditCard', N'', N'MASTERCARD')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'3745', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'43184', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'5489', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'69493', N'Debit', N'BBVA', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'69997', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'82087', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'93945', N'Paganza', N'', N'')
INSERT [dbo].[PaymentMethods] ([Id], [Type], [Bank], [Company]) VALUES (N'96091', N'Debit', N'ITAU', N'')
GO
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Amarillo')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Azul')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (4, N'Azul')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (7, N'Azul')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Blanco')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (5, N'Blanco')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (1, N'Negro')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Negro')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (2, N'Rojo')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Rojo')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (4, N'Rojo')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (7, N'Rojo')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (2, N'Verde')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (3, N'Verde')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (5, N'Verde')
INSERT [dbo].[ProductColors] ([ProductId], [ColourName]) VALUES (6, N'Verde')
GO
SET IDENTITY_INSERT [dbo].[ProductEntities] ON 

INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (1, N'Batsuit', 21, N'Batsuit from THE batboy', 0, N'Nike', N'Shorts', 120)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (2, N'Robin suit', 500, N'The suit that used Batman, maybe', 1, N'Addidas', N'Shorts', 8)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (3, N'Camiseta Malvin', 999, N'LA camiseta', 0, N'ORT', N'Camisa', 11)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (4, N'Zapatos de Harley Quinn', 2000, N'Brand new harlss', 0, N'Nike', N'Accesorios', 5)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (5, N'Camiseta para Hienas', 450, N'Camisetas para hienas como las de Harley', 0, N'Zara', N'Mascotas', 12)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (6, N'Anillo de Linterna Verde', 69, N'Traer a la realidad tu imaginacion con el poder de tu voluntad (hasta que se acabe el stock)', 0, N'Puma', N'Accesorios', 4444)
INSERT [dbo].[ProductEntities] ([Id], [Name], [Price], [Description], [Exclude], [BrandName], [CategoryName], [Stock]) VALUES (7, N'Super capa', 45, N'Capa de homelander o de superman, el que quieras', 1, N'ORT', N'Pantalones', 1)
SET IDENTITY_INSERT [dbo].[ProductEntities] OFF
GO
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (2, N'Batsuit', 3, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (4, N'Batsuit', 3, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (5, N'Batsuit', 5, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (6, N'Batsuit', 4, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (7, N'Batsuit', 61, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (8, N'Batsuit', 2, 1000)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (8, N'Camiseta Malvin', 1, 999)
INSERT [dbo].[PurchasedProductEntity] ([PurchaseId], [ProductName], [Amount], [ProductPrice]) VALUES (8, N'Robin suit', 2, 500)
GO
SET IDENTITY_INSERT [dbo].[PurchaseEntities] ON 

INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (2, N'3X1 Fidelity', CAST(N'2023-11-12T18:16:42.6401078' AS DateTime2), 900, 2100, N'43184', 1)
INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (4, N'3X1 Fidelity', CAST(N'2023-11-13T21:24:41.8316576' AS DateTime2), 900, 2100, N'82087', 2)
INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (5, N'3X1 Fidelity', CAST(N'2023-11-13T21:25:34.0223473' AS DateTime2), 2700, 2300, N'69997', 2)
INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (6, N'3X1 Fidelity', CAST(N'2023-11-14T20:04:55.3967249' AS DateTime2), 2000, 2000, N'96091', 1)
INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (7, N'3X1 Fidelity', CAST(N'2023-11-14T20:06:12.8874673' AS DateTime2), 53100, 7900, N'93945', 1)
INSERT [dbo].[PurchaseEntities] ([Id], [AppliedPromotion], [Date], [FinalPrice], [MoneyDiscounted], [PaymentMethodId], [UserId]) VALUES (8, N'Total look', CAST(N'2023-11-14T20:10:16.3882672' AS DateTime2), 3499.5, 499.5, N'69493', 1)
SET IDENTITY_INSERT [dbo].[PurchaseEntities] OFF
GO
INSERT [dbo].[RoleEntities] ([Name]) VALUES (N'Admin')
INSERT [dbo].[RoleEntities] ([Name]) VALUES (N'Customer')
GO
SET IDENTITY_INSERT [dbo].[UserEntities] ON 

INSERT [dbo].[UserEntities] ([Id], [Email], [Address], [Password], [Token]) VALUES (1, N'bwayne@gmail.com', N'Gotham City', N'batman', N'tokenbwayne@gmail.comsecure')
INSERT [dbo].[UserEntities] ([Id], [Email], [Address], [Password], [Token]) VALUES (2, N'admin@gmail.com', N'admin5', N'admin', N'tokenadmin@gmail.comsecure')
INSERT [dbo].[UserEntities] ([Id], [Email], [Address], [Password], [Token]) VALUES (3, N'clarkent@gmail.com', N'Metropolis', N'superman', N'tokenclarkent@gmail.comsecure')
INSERT [dbo].[UserEntities] ([Id], [Email], [Address], [Password], [Token]) VALUES (4, N'diana@gmail.com', N'Themyscira', N'wonderwoman', N'tokendiana@gmail.comsecure')
INSERT [dbo].[UserEntities] ([Id], [Email], [Address], [Password], [Token]) VALUES (5, N'harls@hotmail.com', N'Arkham', N'ivy<3', N'tokenharls@hotmail.comsecure')
SET IDENTITY_INSERT [dbo].[UserEntities] OFF
GO
/****** Object:  Index [IX_AssignedRoles_UserId]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_AssignedRoles_UserId] ON [dbo].[AssignedRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductColors_ColourName]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_ProductColors_ColourName] ON [dbo].[ProductColors]
(
	[ColourName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_BrandName]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_ProductEntities_BrandName] ON [dbo].[ProductEntities]
(
	[BrandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_CategoryName]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_ProductEntities_CategoryName] ON [dbo].[ProductEntities]
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_Name]    Script Date: 16/11/2023 15:17:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductEntities_Name] ON [dbo].[ProductEntities]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PurchaseEntities_PaymentMethodId]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseEntities_PaymentMethodId] ON [dbo].[PurchaseEntities]
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseEntities_UserId]    Script Date: 16/11/2023 15:17:00 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseEntities_UserId] ON [dbo].[PurchaseEntities]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserEntities_Email]    Script Date: 16/11/2023 15:17:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserEntities_Email] ON [dbo].[UserEntities]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PurchaseEntities] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[AssignedRoles]  WITH CHECK ADD  CONSTRAINT [FK_AssignedRoles_RoleEntities_RoleName] FOREIGN KEY([RoleName])
REFERENCES [dbo].[RoleEntities] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignedRoles] CHECK CONSTRAINT [FK_AssignedRoles_RoleEntities_RoleName]
GO
ALTER TABLE [dbo].[AssignedRoles]  WITH CHECK ADD  CONSTRAINT [FK_AssignedRoles_UserEntities_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignedRoles] CHECK CONSTRAINT [FK_AssignedRoles_UserEntities_UserId]
GO
ALTER TABLE [dbo].[ProductColors]  WITH CHECK ADD  CONSTRAINT [FK_ProductColors_ColourEntities_ColourName] FOREIGN KEY([ColourName])
REFERENCES [dbo].[ColourEntities] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductColors] CHECK CONSTRAINT [FK_ProductColors_ColourEntities_ColourName]
GO
ALTER TABLE [dbo].[ProductColors]  WITH CHECK ADD  CONSTRAINT [FK_ProductColors_ProductEntities_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductColors] CHECK CONSTRAINT [FK_ProductColors_ProductEntities_ProductId]
GO
ALTER TABLE [dbo].[ProductEntities]  WITH CHECK ADD  CONSTRAINT [FK_ProductEntities_BrandEntities_BrandName] FOREIGN KEY([BrandName])
REFERENCES [dbo].[BrandEntities] ([Name])
GO
ALTER TABLE [dbo].[ProductEntities] CHECK CONSTRAINT [FK_ProductEntities_BrandEntities_BrandName]
GO
ALTER TABLE [dbo].[ProductEntities]  WITH CHECK ADD  CONSTRAINT [FK_ProductEntities_CategoryEntities_CategoryName] FOREIGN KEY([CategoryName])
REFERENCES [dbo].[CategoryEntities] ([Name])
GO
ALTER TABLE [dbo].[ProductEntities] CHECK CONSTRAINT [FK_ProductEntities_CategoryEntities_CategoryName]
GO
ALTER TABLE [dbo].[PurchasedProductEntity]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedProductEntity_PurchaseEntities_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[PurchaseEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchasedProductEntity] CHECK CONSTRAINT [FK_PurchasedProductEntity_PurchaseEntities_PurchaseId]
GO
ALTER TABLE [dbo].[PurchaseEntities]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntities_PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseEntities] CHECK CONSTRAINT [FK_PurchaseEntities_PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[PurchaseEntities]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseEntities_UserEntities_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseEntities] CHECK CONSTRAINT [FK_PurchaseEntities_UserEntities_UserId]
GO
USE [master]
GO
ALTER DATABASE [ECommerceDB] SET  READ_WRITE 
GO
