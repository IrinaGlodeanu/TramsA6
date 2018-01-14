using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "MeansOfTransport",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdentifyingCode = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_MeansOfTransport", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Trust = table.Column<double>(nullable: false),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "Comments",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    TransportationMeanId = table.Column<Guid>(nullable: true),
                    Trust = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        "FK_Comments_MeansOfTransport_TransportationMeanId",
                        x => x.TransportationMeanId,
                        "MeansOfTransport",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Comments_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Comments_TransportationMeanId",
                "Comments",
                "TransportationMeanId");

            migrationBuilder.CreateIndex(
                "IX_Comments_UserId",
                "Comments",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Comments");

            migrationBuilder.DropTable(
                "MeansOfTransport");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}