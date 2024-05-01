using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DC_REST.Migrations
{
    /// <inheritdoc />
    public partial class changename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_Issues_IssuesId",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_Labels_LabelsId",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_Issues_IssueId",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_Labels_LabelId",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Issues_IssueId",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Labels",
                table: "Labels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "tbl_user");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "tbl_note");

            migrationBuilder.RenameTable(
                name: "Labels",
                newName: "tbl_label");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "tbl_issue");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Login",
                table: "tbl_user",
                newName: "IX_tbl_user_Login");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_IssueId",
                table: "tbl_note",
                newName: "IX_tbl_note_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_Name",
                table: "tbl_label",
                newName: "IX_tbl_label_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_UserId",
                table: "tbl_issue",
                newName: "IX_tbl_issue_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Title",
                table: "tbl_issue",
                newName: "IX_tbl_issue_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_user",
                table: "tbl_user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_note",
                table: "tbl_note",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_label",
                table: "tbl_label",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_issue",
                table: "tbl_issue",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_tbl_issue_IssueId",
                table: "Issue_Label",
                column: "IssueId",
                principalTable: "tbl_issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_tbl_label_LabelId",
                table: "Issue_Label",
                column: "LabelId",
                principalTable: "tbl_label",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_issue_tbl_user_UserId",
                table: "tbl_issue",
                column: "UserId",
                principalTable: "tbl_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_note_tbl_issue_IssueId",
                table: "tbl_note",
                column: "IssueId",
                principalTable: "tbl_issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_issue_IssuesId",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueLabel_tbl_label_LabelsId",
                table: "IssueLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_tbl_issue_IssueId",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Label_tbl_label_LabelId",
                table: "Issue_Label");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_issue_tbl_user_UserId",
                table: "tbl_issue");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_note_tbl_issue_IssueId",
                table: "tbl_note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_user",
                table: "tbl_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_note",
                table: "tbl_note");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_label",
                table: "tbl_label");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_issue",
                table: "tbl_issue");

            migrationBuilder.RenameTable(
                name: "tbl_user",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "tbl_note",
                newName: "Notes");

            migrationBuilder.RenameTable(
                name: "tbl_label",
                newName: "Labels");

            migrationBuilder.RenameTable(
                name: "tbl_issue",
                newName: "Issues");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_user_Login",
                table: "Users",
                newName: "IX_Users_Login");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_note_IssueId",
                table: "Notes",
                newName: "IX_Notes_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_label_Name",
                table: "Labels",
                newName: "IX_Labels_Name");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_issue_UserId",
                table: "Issues",
                newName: "IX_Issues_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_issue_Title",
                table: "Issues",
                newName: "IX_Issues_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Labels",
                table: "Labels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_Issues_IssuesId",
                table: "IssueLabel",
                column: "IssuesId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueLabel_Labels_LabelsId",
                table: "IssueLabel",
                column: "LabelsId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_Issues_IssueId",
                table: "Issue_Label",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Label_Labels_LabelId",
                table: "Issue_Label",
                column: "LabelId",
                principalTable: "Labels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Issues_IssueId",
                table: "Notes",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
