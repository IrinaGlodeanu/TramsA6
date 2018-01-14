using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class seventhmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments");

            migrationBuilder.DropColumn(
                "TransportationMeanId",
                "Comments");

            migrationBuilder.AlterColumn<Guid>(
                "TransportMeanId",
                "Comments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments",
                "TransportMeanId",
                "MeansOfTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments");

            migrationBuilder.AlterColumn<Guid>(
                "TransportMeanId",
                "Comments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                "TransportationMeanId",
                "Comments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments",
                "TransportMeanId",
                "MeansOfTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}