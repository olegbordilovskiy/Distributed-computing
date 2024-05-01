using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DC_REST.Migrations
{
    /// <inheritdoc />
    public partial class fixregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "tbl_user",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "tbl_user",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "tbl_user",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "tbl_user",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_user",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_user_Login",
                table: "tbl_user",
                newName: "IX_tbl_user_login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "tbl_user",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "tbl_user",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "tbl_user",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "tbl_user",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_user",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_user_login",
                table: "tbl_user",
                newName: "IX_tbl_user_Login");
        }
    }
}
