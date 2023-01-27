using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRefactoredInitial6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserUserEmail",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserUserEmail",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserUserEmail",
                table: "Orders",
                newName: "IX_Orders_UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "UserUserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserEmail",
                table: "Orders",
                newName: "IX_Orders_UserUserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserUserEmail",
                table: "Orders",
                column: "UserUserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
