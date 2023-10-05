USE [master]
GO
/****** Object:  Database [ECommerceDB]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE DATABASE [ECommerceDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ECommerceDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EmptyDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ECommerceDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EmptyDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 05-Oct-23 2:05:53 PM ******/
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
/****** Object:  Table [dbo].[AssignedRoles]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignedRoles](
	[RoleName] [nvarchar](450) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AssignedRoles] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
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
/****** Object:  Table [dbo].[CategoryEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
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
/****** Object:  Table [dbo].[ColourEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
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
/****** Object:  Table [dbo].[ProductColors]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductColors](
	[ColourName] [nvarchar](450) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductColors] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[ColourName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductEntities](
	[Name] [nvarchar](450) NOT NULL,
	[Price] [float] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[BrandName] [nvarchar](450) NULL,
	[CategoryName] [nvarchar](450) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ProductEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchasedProductEntity]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasedProductEntity](
	[PurchaseId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_PurchasedProductEntity] PRIMARY KEY CLUSTERED 
(
	[PurchaseId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppliedPromotion] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
	[FinalPrice] [float] NOT NULL,
	[MoneyDiscounted] [float] NOT NULL,
 CONSTRAINT [PK_PurchaseEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
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
/****** Object:  Table [dbo].[UserEntities]    Script Date: 05-Oct-23 2:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEntities](
	[Email] [nvarchar](450) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230905231524_CreateECommerceDb', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230906225505_v2', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230906231110_v3', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230910233037_v4', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230922222338_v0', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230927033459_V5', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230927232711_v7', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231003030014_v8', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231003131531_roles', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231003184914_product-name-not-ak', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231003230735_purchase-entity-with-price', N'6.0.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231005002928_user-refactor', N'6.0.21')
GO
INSERT [dbo].[AssignedRoles] ([RoleName], [UserId]) VALUES (N'Admin', 1)
INSERT [dbo].[AssignedRoles] ([RoleName], [UserId]) VALUES (N'Customer', 1)
GO
INSERT [dbo].[RoleEntities] ([Name]) VALUES (N'Admin')
INSERT [dbo].[RoleEntities] ([Name]) VALUES (N'Customer')
GO
SET IDENTITY_INSERT [dbo].[UserEntities] ON 

INSERT [dbo].[UserEntities] ([Email], [Address], [Id], [Password], [Token]) VALUES (N'admin@gmail.com', N'MojoDojoCasaHouse', 1, N'admin', N'tokenadmin@gmail.comsecure')
SET IDENTITY_INSERT [dbo].[UserEntities] OFF
GO
/****** Object:  Index [IX_AssignedRoles_UserId]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_AssignedRoles_UserId] ON [dbo].[AssignedRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductColors_ColourName]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductColors_ColourName] ON [dbo].[ProductColors]
(
	[ColourName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_BrandName]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductEntities_BrandName] ON [dbo].[ProductEntities]
(
	[BrandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_CategoryName]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductEntities_CategoryName] ON [dbo].[ProductEntities]
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductEntities_Name]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductEntities_Name] ON [dbo].[ProductEntities]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchasedProductEntity_ProductId]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchasedProductEntity_ProductId] ON [dbo].[PurchasedProductEntity]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseEntities_UserId]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseEntities_UserId] ON [dbo].[PurchaseEntities]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserEntities_Email]    Script Date: 05-Oct-23 2:05:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserEntities_Email] ON [dbo].[UserEntities]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AssignedRoles] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[ProductColors] ADD  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[PurchasedProductEntity] ADD  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[PurchaseEntities] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[PurchaseEntities] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [FinalPrice]
GO
ALTER TABLE [dbo].[PurchaseEntities] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [MoneyDiscounted]
GO
ALTER TABLE [dbo].[UserEntities] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[UserEntities] ADD  DEFAULT (N'') FOR [Token]
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
ALTER TABLE [dbo].[PurchasedProductEntity]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedProductEntity_ProductEntities_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchasedProductEntity] CHECK CONSTRAINT [FK_PurchasedProductEntity_ProductEntities_ProductId]
GO
ALTER TABLE [dbo].[PurchasedProductEntity]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedProductEntity_PurchaseEntities_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[PurchaseEntities] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchasedProductEntity] CHECK CONSTRAINT [FK_PurchasedProductEntity_PurchaseEntities_PurchaseId]
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
