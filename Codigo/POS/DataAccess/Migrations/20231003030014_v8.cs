using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedRoles_RoleEntity_RoleName",
                table: "AssignedRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity");

            migrationBuilder.RenameTable(
                name: "RoleEntity",
                newName: "RoleEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEntities",
                table: "RoleEntities",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedRoles_RoleEntities_RoleName",
                table: "AssignedRoles",
                column: "RoleName",
                principalTable: "RoleEntities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignedRoles_RoleEntities_RoleName",
                table: "AssignedRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleEntities",
                table: "RoleEntities");

            migrationBuilder.RenameTable(
                name: "RoleEntities",
                newName: "RoleEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleEntity",
                table: "RoleEntity",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignedRoles_RoleEntity_RoleName",
                table: "AssignedRoles",
                column: "RoleName",
                principalTable: "RoleEntity",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
