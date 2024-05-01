using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DC_REST.Migrations
{
    /// <inheritdoc />
    public partial class fixallidregisters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_issue_IssuesId",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_label_LabelsId",
                table: "IssueLabel");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_note",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_label",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbl_issue",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LabelsId",
                table: "IssueLabel",
                newName: "Labelsid");

            migrationBuilder.RenameColumn(
                name: "IssuesId",
                table: "IssueLabel",
                newName: "Issuesid");

            migrationBuilder.RenameIndex(
                name: "IX_IssueLabel_LabelsId",
                table: "IssueLabel",
                newName: "IX_IssueLabel_Labelsid");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_tbl_issue_Issuesid",
                table: "IssueLabel",
                column: "Issuesid",
                principalTable: "tbl_issue",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_tbl_label_Labelsid",
                table: "IssueLabel",
                column: "Labelsid",
                principalTable: "tbl_label",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_issue_Issuesid",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_label_Labelsid",
                table: "IssueLabel");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_note",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_label",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbl_issue",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Labelsid",
                table: "IssueLabel",
                newName: "LabelsId");

            migrationBuilder.RenameColumn(
                name: "Issuesid",
                table: "IssueLabel",
                newName: "IssuesId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueLabel_Labelsid",
                table: "IssueLabel",
                newName: "IX_IssueLabel_LabelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_tbl_issue_IssuesId",
                table: "IssueLabel",
                column: "IssuesId",
                principalTable: "tbl_issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_tbl_label_LabelsId",
                table: "IssueLabel",
                column: "LabelsId",
                principalTable: "tbl_label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
