using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DC_REST.Migrations
{
    /// <inheritdoc />
    public partial class issueid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_tbl_issue_IssueId",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_note_tbl_issue_IssueId",
                table: "tbl_note");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "tbl_note",
                newName: "issue_id");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_note_IssueId",
                table: "tbl_note",
                newName: "IX_tbl_note_issue_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Issue_Label",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IssueId",
                table: "Issue_Label",
                newName: "issue_id");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_Label_IssueId",
                table: "Issue_Label",
                newName: "IX_Issue_Label_issue_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_tbl_issue_issue_id",
                table: "Issue_Label",
                column: "issue_id",
                principalTable: "tbl_issue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_note_tbl_issue_issue_id",
                table: "tbl_note",
                column: "issue_id",
                principalTable: "tbl_issue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_tbl_issue_issue_id",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_note_tbl_issue_issue_id",
                table: "tbl_note");

            migrationBuilder.RenameColumn(
                name: "issue_id",
                table: "tbl_note",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_note_issue_id",
                table: "tbl_note",
                newName: "IX_tbl_note_IssueId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Issue_Label",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "issue_id",
                table: "Issue_Label",
                newName: "IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_Label_issue_id",
                table: "Issue_Label",
                newName: "IX_Issue_Label_IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_tbl_issue_IssueId",
                table: "Issue_Label",
                column: "IssueId",
                principalTable: "tbl_issue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_note_tbl_issue_IssueId",
                table: "tbl_note",
                column: "IssueId",
                principalTable: "tbl_issue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
