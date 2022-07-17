using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginForm.Migrations
{
    public partial class Add_Account_To_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Accounts_AccountId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Comments",
                newName: "FromId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AccountId",
                table: "Comments",
                newName: "IX_Comments_FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Accounts_FromId",
                table: "Comments",
                column: "FromId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Accounts_FromId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "FromId",
                table: "Comments",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_FromId",
                table: "Comments",
                newName: "IX_Comments_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Accounts_AccountId",
                table: "Comments",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
