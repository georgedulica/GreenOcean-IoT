using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Migrations
{
    /// <inheritdoc />
    public partial class Initialization2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GreenHouses_Users_UserId",
                table: "GreenHouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GreenHouses",
                table: "GreenHouses");

            migrationBuilder.RenameTable(
                name: "GreenHouses",
                newName: "Greenhouses");

            migrationBuilder.RenameIndex(
                name: "IX_GreenHouses_UserId",
                table: "Greenhouses",
                newName: "IX_Greenhouses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Greenhouses",
                table: "Greenhouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Greenhouses_Users_UserId",
                table: "Greenhouses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greenhouses_Users_UserId",
                table: "Greenhouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Greenhouses",
                table: "Greenhouses");

            migrationBuilder.RenameTable(
                name: "Greenhouses",
                newName: "GreenHouses");

            migrationBuilder.RenameIndex(
                name: "IX_Greenhouses_UserId",
                table: "GreenHouses",
                newName: "IX_GreenHouses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GreenHouses",
                table: "GreenHouses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GreenHouses_Users_UserId",
                table: "GreenHouses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
