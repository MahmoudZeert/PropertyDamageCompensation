using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class UpdateApplicationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppDate",
                table: "Application",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AppNumber",
                table: "Application",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Application",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationState", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_StateId",
                table: "Application",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application",
                column: "StateId",
                principalTable: "ApplicationState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application");

            migrationBuilder.DropTable(
                name: "ApplicationState");

            migrationBuilder.DropIndex(
                name: "IX_Application_StateId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "AppDate",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "AppNumber",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Application");
        }
    }
}
