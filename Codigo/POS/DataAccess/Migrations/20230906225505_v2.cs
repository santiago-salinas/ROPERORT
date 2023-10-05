using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColourEnityEntities_ProductEntities_ProductEntityName",
                table: "ColourEnityEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColourEnityEntities",
                table: "ColourEnityEntities");

            migrationBuilder.RenameTable(
                name: "ColourEnityEntities",
                newName: "ColourEntities");

            migrationBuilder.RenameIndex(
                name: "IX_ColourEnityEntities_ProductEntityName",
                table: "ColourEntities",
                newName: "IX_ColourEntities_ProductEntityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColourEntities",
                table: "ColourEntities",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "PurchaseEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppliedPromotion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseEntities_UserEntities_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "UserEntities",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleEntity",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserEntityEmail = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntity", x => x.Name);
                    table.ForeignKey(
                        name: "FK_RoleEntity_UserEntities_UserEntityEmail",
                        column: x => x.UserEntityEmail,
                        principalTable: "UserEntities",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "PurchasedProductEntity",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedProductEntity", x => new { x.PurchaseId, x.ProductName });
                    table.ForeignKey(
                        name: "FK_PurchasedProductEntity_ProductEntities_ProductName",
                        column: x => x.ProductName,
                        principalTable: "ProductEntities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedProductEntity_PurchaseEntities_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "PurchaseEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedProductEntity_ProductName",
                table: "PurchasedProductEntity",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_UserEmail",
                table: "PurchaseEntities",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntity_UserEntityEmail",
                table: "RoleEntity",
                column: "UserEntityEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_ColourEntities_ProductEntities_ProductEntityName",
                table: "ColourEntities",
                column: "ProductEntityName",
                principalTable: "ProductEntities",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColourEntities_ProductEntities_ProductEntityName",
                table: "ColourEntities");

            migrationBuilder.DropTable(
                name: "PurchasedProductEntity");

            migrationBuilder.DropTable(
                name: "RoleEntity");

            migrationBuilder.DropTable(
                name: "PurchaseEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColourEntities",
                table: "ColourEntities");

            migrationBuilder.RenameTable(
                name: "ColourEntities",
                newName: "ColourEnityEntities");

            migrationBuilder.RenameIndex(
                name: "IX_ColourEntities_ProductEntityName",
                table: "ColourEnityEntities",
                newName: "IX_ColourEnityEntities_ProductEntityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColourEnityEntities",
                table: "ColourEnityEntities",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ColourEnityEntities_ProductEntities_ProductEntityName",
                table: "ColourEnityEntities",
                column: "ProductEntityName",
                principalTable: "ProductEntities",
                principalColumn: "Name");
        }
    }
}
