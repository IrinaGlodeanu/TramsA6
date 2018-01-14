using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class sixthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_MeansOfTransport_TransportationMeanId",
                "Comments");

            migrationBuilder.DropForeignKey(
                "FK_Comments_Users_UserId",
                "Comments");

            migrationBuilder.DropIndex(
                "IX_Comments_TransportationMeanId",
                "Comments");

            migrationBuilder.AlterColumn<Guid>(
                "UserId",
                "Comments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                "TransportationMeanId",
                "Comments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                "TransportMeanId",
                "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Comments_TransportMeanId",
                "Comments",
                "TransportMeanId");

            migrationBuilder.AddForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments",
                "TransportMeanId",
                "MeansOfTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Comments_Users_UserId",
                "Comments",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Comments_MeansOfTransport_TransportMeanId",
                "Comments");

            migrationBuilder.DropForeignKey(
                "FK_Comments_Users_UserId",
                "Comments");

            migrationBuilder.DropIndex(
                "IX_Comments_TransportMeanId",
                "Comments");

            migrationBuilder.DropColumn(
                "TransportMeanId",
                "Comments");

            migrationBuilder.AlterColumn<Guid>(
                "UserId",
                "Comments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                "TransportationMeanId",
                "Comments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                "IX_Comments_TransportationMeanId",
                "Comments",
                "TransportationMeanId");

            migrationBuilder.AddForeignKey(
                "FK_Comments_MeansOfTransport_TransportationMeanId",
                "Comments",
                "TransportationMeanId",
                "MeansOfTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Comments_Users_UserId",
                "Comments",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}