using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Data.Migrations
{
    /// <inheritdoc />
    public partial class Equipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegisteredEquipmentId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_RegisteredEquipmentId",
                table: "Equipments",
                column: "RegisteredEquipmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_RegisteredEquipments_RegisteredEquipmentId",
                table: "Equipments",
                column: "RegisteredEquipmentId",
                principalTable: "RegisteredEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_RegisteredEquipments_RegisteredEquipmentId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_RegisteredEquipmentId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "RegisteredEquipmentId",
                table: "Equipments");
        }
    }
}
