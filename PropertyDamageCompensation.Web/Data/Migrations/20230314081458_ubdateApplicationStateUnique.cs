using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class ubdateApplicationStateUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ApplicationState_Name",
                table: "ApplicationState");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationState_Name",
                table: "ApplicationState",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationState_Name",
                table: "ApplicationState");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ApplicationState_Name",
                table: "ApplicationState",
                column: "Name");
        }
    }
}
