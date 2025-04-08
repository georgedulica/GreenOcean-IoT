using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorData_Equipments_IoTSystemId",
                table: "SensorData");

            migrationBuilder.RenameColumn(
                name: "IoTSystemId",
                table: "SensorData",
                newName: "EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorData_IoTSystemId",
                table: "SensorData",
                newName: "IX_SensorData_EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorData_Equipments_EquipmentId",
                table: "SensorData",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorData_Equipments_EquipmentId",
                table: "SensorData");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "SensorData",
                newName: "IoTSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_SensorData_EquipmentId",
                table: "SensorData",
                newName: "IX_SensorData_IoTSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorData_Equipments_IoTSystemId",
                table: "SensorData",
                column: "IoTSystemId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
