﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20231112194847_payment2")]
    partial class payment2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.AssignedRoles", b =>
                {
                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RoleName", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignedRoles");
                });

            modelBuilder.Entity("DataAccess.Entities.BrandEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("BrandEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.CategoryEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("CategoryEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.ColourEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("ColourEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.PaymentMethodEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("DataAccess.Entities.ProductColors", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ColourName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductId", "ColourName");

                    b.HasIndex("ColourName");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("DataAccess.Entities.ProductEntity", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Exclude")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandName");

                    b.HasIndex("CategoryName");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProductEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.PurchasedProductEntity", b =>
                {
                    b.Property<int>("PurchaseId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("PurchaseId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("PurchasedProductEntity");
                });

            modelBuilder.Entity("DataAccess.Entities.PurchaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AppliedPromotion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<double>("MoneyDiscounted")
                        .HasColumnType("float");

                    b.Property<string>("PaymentMethodId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.ToTable("PurchaseEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.RoleEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("RoleEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UserEntities");
                });

            modelBuilder.Entity("DataAccess.Entities.AssignedRoles", b =>
                {
                    b.HasOne("DataAccess.Entities.RoleEntity", "Role")
                        .WithMany("AssignedRoles")
                        .HasForeignKey("RoleName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserEntity", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.ProductColors", b =>
                {
                    b.HasOne("DataAccess.Entities.ColourEntity", "Colour")
                        .WithMany("ProductColors")
                        .HasForeignKey("ColourName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.ProductEntity", "Product")
                        .WithMany("Colours")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colour");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataAccess.Entities.ProductEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.BrandEntity", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandName");

                    b.HasOne("DataAccess.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryName");

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccess.Entities.PurchasedProductEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.PurchaseEntity", "Purchase")
                        .WithMany("Items")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("DataAccess.Entities.PurchaseEntity", b =>
                {
                    b.HasOne("DataAccess.Entities.PaymentMethodEntity", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMethod");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.ColourEntity", b =>
                {
                    b.Navigation("ProductColors");
                });

            modelBuilder.Entity("DataAccess.Entities.ProductEntity", b =>
                {
                    b.Navigation("Colours");
                });

            modelBuilder.Entity("DataAccess.Entities.PurchaseEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DataAccess.Entities.RoleEntity", b =>
                {
                    b.Navigation("AssignedRoles");
                });

            modelBuilder.Entity("DataAccess.Entities.UserEntity", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
