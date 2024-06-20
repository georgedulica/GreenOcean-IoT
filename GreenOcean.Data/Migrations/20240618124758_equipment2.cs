using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Data.Migrations
{
    /// <inheritdoc />
    public partial class equipment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Greenhouses_GreenhouseId",
                table: "Equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_RegisteredEquipments_RegisteredEquipmentId",
                table: "Equipments");

            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisteredEquipments",
                table: "RegisteredEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments");

            migrationBuilder.RenameTable(
                name: "RegisteredEquipments",
                newName: "RegisteredEquipment");

            migrationBuilder.RenameTable(
                name: "Equipments",
                newName: "Equipment");

            migrationBuilder.RenameIndex(
                name: "IX_Equipments_RegisteredEquipmentId",
                table: "Equipment",
                newName: "IX_Equipment_RegisteredEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipments_GreenhouseId",
                table: "Equipment",
                newName: "IX_Equipment_GreenhouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisteredEquipment",
                table: "RegisteredEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Greenhouses_GreenhouseId",
                table: "Equipment",
                column: "GreenhouseId",
                principalTable: "Greenhouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_RegisteredEquipment_RegisteredEquipmentId",
                table: "Equipment",
                column: "RegisteredEquipmentId",
                principalTable: "RegisteredEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Greenhouses_GreenhouseId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_RegisteredEquipment_RegisteredEquipmentId",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisteredEquipment",
                table: "RegisteredEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment");

            migrationBuilder.RenameTable(
                name: "RegisteredEquipment",
                newName: "RegisteredEquipments");

            migrationBuilder.RenameTable(
                name: "Equipment",
                newName: "Equipments");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_RegisteredEquipmentId",
                table: "Equipments",
                newName: "IX_Equipments_RegisteredEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipment_GreenhouseId",
                table: "Equipments",
                newName: "IX_Equipments_GreenhouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisteredEquipments",
                table: "RegisteredEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipments",
                table: "Equipments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    LightLevel = table.Column<float>(type: "real", nullable: false),
                    SoilMoisture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorData_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_EquipmentId",
                table: "SensorData",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Greenhouses_GreenhouseId",
                table: "Equipments",
                column: "GreenhouseId",
                principalTable: "Greenhouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_RegisteredEquipments_RegisteredEquipmentId",
                table: "Equipments",
                column: "RegisteredEquipmentId",
                principalTable: "RegisteredEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
