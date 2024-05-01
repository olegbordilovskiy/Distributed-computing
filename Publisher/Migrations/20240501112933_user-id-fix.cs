using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DC_REST.Migrations
{
    /// <inheritdoc />
    public partial class useridfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_issue_tbl_user_UserId",
                table: "tbl_issue");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tbl_issue",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_issue_UserId",
                table: "tbl_issue",
                newName: "IX_tbl_issue_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_issue_tbl_user_user_id",
                table: "tbl_issue",
                column: "user_id",
                principalTable: "tbl_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_issue_tbl_user_user_id",
                table: "tbl_issue");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "tbl_issue",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_issue_user_id",
                table: "tbl_issue",
                newName: "IX_tbl_issue_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_issue_tbl_user_UserId",
                table: "tbl_issue",
                column: "UserId",
                principalTable: "tbl_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
