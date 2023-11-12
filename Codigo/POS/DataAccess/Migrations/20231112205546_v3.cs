using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PurchaseEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_UserId",
                table: "PurchaseEntities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntities_UserEntities_UserId",
                table: "PurchaseEntities",
                column: "UserId",
                principalTable: "UserEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntities_UserEntities_UserId",
                table: "PurchaseEntities");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntities_UserId",
                table: "PurchaseEntities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PurchaseEntities");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "PurchaseEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
