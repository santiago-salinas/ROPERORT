using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedRoles_UserEntities_UserEmail",
                table: "AssignedRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_ProductEntities_ProductName",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedProductEntity_ProductEntities_ProductName",
                table: "PurchasedProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntities_UserEntities_UserEmail",
                table: "PurchaseEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntities_UserEmail",
                table: "PurchaseEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedProductEntity",
                table: "PurchasedProductEntity");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedProductEntity_ProductName",
                table: "PurchasedProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntities",
                table: "ProductEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedRoles",
                table: "AssignedRoles");

            migrationBuilder.DropIndex(
                name: "IX_AssignedRoles_UserEmail",
                table: "AssignedRoles");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "PurchaseEntities");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "PurchasedProductEntity");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "AssignedRoles");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserEntities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "AppliedPromotion",
                table: "PurchaseEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PurchaseEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PurchasedProductEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "ProductEntities",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AssignedRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserEntities_Email",
                table: "UserEntities",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedProductEntity",
                table: "PurchasedProductEntity",
                columns: new[] { "PurchaseId", "ProductId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProductEntities_Name",
                table: "ProductEntities",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntities",
                table: "ProductEntities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ProductId", "ColourName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedRoles",
                table: "AssignedRoles",
                columns: new[] { "RoleName", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_UserId",
                table: "PurchaseEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedProductEntity_ProductId",
                table: "PurchasedProductEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRoles_UserId",
                table: "AssignedRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedRoles_UserEntities_UserId",
                table: "AssignedRoles",
                column: "UserId",
                principalTable: "UserEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_ProductEntities_ProductId",
                table: "ProductColors",
                column: "ProductId",
                principalTable: "ProductEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedProductEntity_ProductEntities_ProductId",
                table: "PurchasedProductEntity",
                column: "ProductId",
                principalTable: "ProductEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_AssignedRoles_UserEntities_UserId",
                table: "AssignedRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_ProductEntities_ProductId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedProductEntity_ProductEntities_ProductId",
                table: "PurchasedProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntities_UserEntities_UserId",
                table: "PurchaseEntities");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserEntities_Email",
                table: "UserEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntities_UserId",
                table: "PurchaseEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedProductEntity",
                table: "PurchasedProductEntity");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedProductEntity_ProductId",
                table: "PurchasedProductEntity");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProductEntities_Name",
                table: "ProductEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductEntities",
                table: "ProductEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedRoles",
                table: "AssignedRoles");

            migrationBuilder.DropIndex(
                name: "IX_AssignedRoles_UserId",
                table: "AssignedRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PurchaseEntities");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PurchasedProductEntity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AssignedRoles");

            migrationBuilder.AlterColumn<string>(
                name: "AppliedPromotion",
                table: "PurchaseEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "PurchaseEntities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "PurchasedProductEntity",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductColors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "AssignedRoles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedProductEntity",
                table: "PurchasedProductEntity",
                columns: new[] { "PurchaseId", "ProductName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductEntities",
                table: "ProductEntities",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductColors",
                table: "ProductColors",
                columns: new[] { "ProductName", "ColourName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedRoles",
                table: "AssignedRoles",
                columns: new[] { "RoleName", "UserEmail" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntities_UserEmail",
                table: "PurchaseEntities",
                column: "UserEmail");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedProductEntity_ProductName",
                table: "PurchasedProductEntity",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedRoles_UserEmail",
                table: "AssignedRoles",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedRoles_UserEntities_UserEmail",
                table: "AssignedRoles",
                column: "UserEmail",
                principalTable: "UserEntities",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_ProductEntities_ProductName",
                table: "ProductColors",
                column: "ProductName",
                principalTable: "ProductEntities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedProductEntity_ProductEntities_ProductName",
                table: "PurchasedProductEntity",
                column: "ProductName",
                principalTable: "ProductEntities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntities_UserEntities_UserEmail",
                table: "PurchaseEntities",
                column: "UserEmail",
                principalTable: "UserEntities",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
