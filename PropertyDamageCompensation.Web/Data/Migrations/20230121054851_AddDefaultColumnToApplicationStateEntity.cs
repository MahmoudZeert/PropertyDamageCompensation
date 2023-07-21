using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class AddDefaultColumnToApplicationStateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "ApplicationState",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "ApplicationState");
        }
    }
}
