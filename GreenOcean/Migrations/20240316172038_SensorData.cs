using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Migrations
{
    /// <inheritdoc />
    public partial class SensorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    LightLevel = table.Column<float>(type: "real", nullable: false),
                    SoilMoisture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    IoTSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorData_IoTSystems_IoTSystemId",
                        column: x => x.IoTSystemId,
                        principalTable: "IoTSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_IoTSystemId",
                table: "SensorData",
                column: "IoTSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");
        }
    }
}
