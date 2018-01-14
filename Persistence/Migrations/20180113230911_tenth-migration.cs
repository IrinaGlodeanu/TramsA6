using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class tenthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                "NumberOfVotes",
                "Users",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                "NumberOfVotes",
                "MeansOfTransport",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "NumberOfVotes",
                "MeansOfTransport");

            migrationBuilder.AlterColumn<double>(
                "NumberOfVotes",
                "Users",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}