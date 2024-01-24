using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Migrations
{
    /// <inheritdoc />
    public partial class mkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "LoginHistory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleID",
                table: "LoginHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistory_IdentityRoleId",
                table: "LoginHistory",
                column: "IdentityRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginHistory_AspNetRoles_IdentityRoleId",
                table: "LoginHistory",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginHistory_AspNetRoles_IdentityRoleId",
                table: "LoginHistory");

            migrationBuilder.DropIndex(
                name: "IX_LoginHistory_IdentityRoleId",
                table: "LoginHistory");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "LoginHistory");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "LoginHistory");
        }
    }
}
