using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class payments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandEntities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "CategoryEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ColourEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColourEntities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntities_BrandEntities_BrandName",
                        column: x => x.BrandName,
                        principalTable: "BrandEntities",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_ProductEntities_CategoryEntities_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "CategoryEntities",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "AssignedRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedRoles", x => new { x.RoleName, x.UserId });
                    table.ForeignKey(
                        name: "FK_AssignedRoles_RoleEntities_RoleName",
                        column: x => x.RoleName,
                        principalTable: "RoleEntities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedRoles_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AppliedPromotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalPrice = table.Column<double>(type: "float", nullable: false),
                    MoneyDiscounted = table.Column<double>(type: "float", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseEntities_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseEntities_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColourName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => new { x.ProductId, x.ColourName });
                    table.ForeignKey(
                        name: "FK_ProductColors_ColourEntities_ColourName",
                        column: x => x.ColourName,
                        principalTable: "ColourEntities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColors_ProductEntities_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedProductEntity",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedProductEntity", x => new { x.PurchaseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_PurchasedProductEntity_ProductEntities_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedProductEntity_PurchaseEntities_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "PurchaseEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRoles_UserId",
                table: "AssignedRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ColourName",
                table: "ProductColors",
                column: "ColourName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_BrandName",
                table: "ProductEntities",
                column: "BrandName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_CategoryName",
                table: "ProductEntities",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_Name",
                table: "ProductEntities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedProductEntity_ProductId",
                table: "PurchasedProductEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_PaymentMethodId",
                table: "PurchaseEntities",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_UserId",
                table: "PurchaseEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntities_Email",
                table: "UserEntities",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedRoles");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "PurchasedProductEntity");

            migrationBuilder.DropTable(
                name: "RoleEntities");

            migrationBuilder.DropTable(
                name: "ColourEntities");

            migrationBuilder.DropTable(
                name: "ProductEntities");

            migrationBuilder.DropTable(
                name: "PurchaseEntities");

            migrationBuilder.DropTable(
                name: "BrandEntities");

            migrationBuilder.DropTable(
                name: "CategoryEntities");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
