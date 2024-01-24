using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Migrations
{
    /// <inheritdoc />
    public partial class newdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Members_Roles",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_Roles",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "AspNetUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Members",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastname",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "lastname",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "AspNetUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_Roles",
                table: "AspNetUserRoles",
                column: "Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Members_Roles",
                table: "AspNetUserRoles",
                column: "Roles",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
