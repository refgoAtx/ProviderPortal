using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Provider.Migrations
{
    /// <inheritdoc />
    public partial class teting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
