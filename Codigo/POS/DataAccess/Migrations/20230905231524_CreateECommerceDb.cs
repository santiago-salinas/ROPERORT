using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class CreateECommerceDb : Migration
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
                name: "UserEntities",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "ProductEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntities", x => x.Name);
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
                name: "ColourEnityEntities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductEntityName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColourEnityEntities", x => x.Name);
                    table.ForeignKey(
                        name: "FK_ColourEnityEntities_ProductEntities_ProductEntityName",
                        column: x => x.ProductEntityName,
                        principalTable: "ProductEntities",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColourEnityEntities_ProductEntityName",
                table: "ColourEnityEntities",
                column: "ProductEntityName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_BrandName",
                table: "ProductEntities",
                column: "BrandName");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_CategoryName",
                table: "ProductEntities",
                column: "CategoryName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColourEnityEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");

            migrationBuilder.DropTable(
                name: "ProductEntities");

            migrationBuilder.DropTable(
                name: "BrandEntities");

            migrationBuilder.DropTable(
                name: "CategoryEntities");
        }
    }
}
