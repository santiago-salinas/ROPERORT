using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class productnamenotak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductEntities_Name",
                table: "ProductEntities");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_Name",
                table: "ProductEntities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductEntities_Name",
                table: "ProductEntities");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductEntities_Name",
                table: "ProductEntities",
                column: "Name");
        }
    }
}
