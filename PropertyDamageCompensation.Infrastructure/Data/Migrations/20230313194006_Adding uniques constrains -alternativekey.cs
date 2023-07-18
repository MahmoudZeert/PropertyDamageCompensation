using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class Addinguniquesconstrainsalternativekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyType_Name",
                table: "PropertyType");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PropertyType_Name",
                table: "PropertyType",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_PropertyType_Name",
                table: "PropertyType");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_Name",
                table: "PropertyType",
                column: "Name",
                unique: true);
        }
    }
}
