using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class fourthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Name",
                "Users",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<double>(
                "Location_Latitude",
                "MeansOfTransport",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                "Location_Longitude",
                "MeansOfTransport",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Location_Latitude",
                "MeansOfTransport");

            migrationBuilder.DropColumn(
                "Location_Longitude",
                "MeansOfTransport");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Users",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }
    }
}