using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceBackend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredRefactoredInitial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "UserUserEmail",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserUserEmail",
                table: "Orders",
                column: "UserUserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserUserEmail",
                table: "Orders",
                column: "UserUserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserUserEmail",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserUserEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserUserEmail",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserEmail",
                table: "Orders",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
