using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColourEntities_ProductEntities_ProductEntityName",
                table: "ColourEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleEntity_UserEntities_UserEntityEmail",
                table: "RoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_RoleEntity_UserEntityEmail",
                table: "RoleEntity");

            migrationBuilder.DropIndex(
                name: "IX_ColourEntities_ProductEntityName",
                table: "ColourEntities");

            migrationBuilder.DropColumn(
                name: "UserEntityEmail",
                table: "RoleEntity");

            migrationBuilder.DropColumn(
                name: "ProductEntityName",
                table: "ColourEntities");

            migrationBuilder.CreateTable(
                name: "AssignedRoles",
                columns: table => new
                {
                    UserEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedRoles", x => new { x.RoleName, x.UserEmail });
                    table.ForeignKey(
                        name: "FK_AssignedRoles_RoleEntity_RoleName",
                        column: x => x.RoleName,
                        principalTable: "RoleEntity",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedRoles_UserEntities_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "UserEntities",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ColourName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => new { x.ProductName, x.ColourName });
                    table.ForeignKey(
                        name: "FK_ProductColors_ColourEntities_ColourName",
                        column: x => x.ColourName,
                        principalTable: "ColourEntities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColors_ProductEntities_ProductName",
                        column: x => x.ProductName,
                        principalTable: "ProductEntities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRoles_UserEmail",
                table: "AssignedRoles",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ColourName",
                table: "ProductColors",
                column: "ColourName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedRoles");

            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.AddColumn<string>(
                name: "UserEntityEmail",
                table: "RoleEntity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductEntityName",
                table: "ColourEntities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleEntity_UserEntityEmail",
                table: "RoleEntity",
                column: "UserEntityEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ColourEntities_ProductEntityName",
                table: "ColourEntities",
                column: "ProductEntityName");

            migrationBuilder.AddForeignKey(
                name: "FK_ColourEntities_ProductEntities_ProductEntityName",
                table: "ColourEntities",
                column: "ProductEntityName",
                principalTable: "ProductEntities",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleEntity_UserEntities_UserEntityEmail",
                table: "RoleEntity",
                column: "UserEntityEmail",
                principalTable: "UserEntities",
                principalColumn: "Email");
        }
    }
}
