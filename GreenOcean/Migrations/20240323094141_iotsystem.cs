using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenOcean.Migrations
{
    /// <inheritdoc />
    public partial class iotsystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTemperature",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "MinTemperature",
                table: "Plants");

            migrationBuilder.AlterColumn<float>(
                name: "MositureLevel",
                table: "Plants",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Humidity",
                table: "Plants",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Height",
                table: "Plants",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Temperature",
                table: "Plants",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxHumidity",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxLightLevel",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MaxTemperature",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinHumidity",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinLightLevel",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinTemperature",
                table: "IoTSystems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "MaxHumidity",
                table: "IoTSystems");

            migrationBuilder.DropColumn(
                name: "MaxLightLevel",
                table: "IoTSystems");

            migrationBuilder.DropColumn(
                name: "MaxTemperature",
                table: "IoTSystems");

            migrationBuilder.DropColumn(
                name: "MinHumidity",
                table: "IoTSystems");

            migrationBuilder.DropColumn(
                name: "MinLightLevel",
                table: "IoTSystems");

            migrationBuilder.DropColumn(
                name: "MinTemperature",
                table: "IoTSystems");

            migrationBuilder.AlterColumn<int>(
                name: "MositureLevel",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Humidity",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Plants",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxTemperature",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinTemperature",
                table: "Plants",
                type: "int",
                nullable: true);
        }
    }
}
