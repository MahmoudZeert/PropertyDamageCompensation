using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class ChangeUniqueConstraintFromAlternativeKeyToIHasIndexAndIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Towns_Name",
                table: "Towns");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PropertyType_Name",
                table: "PropertyType");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Floor_Name",
                table: "Floor");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_Name",
                table: "Towns",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyType_Name",
                table: "PropertyType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo",
                columns: new[] { "FirstName", "MiddleName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Floor_Name",
                table: "Floor",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Towns_Name",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_PropertyType_Name",
                table: "PropertyType");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo");

            migrationBuilder.DropIndex(
                name: "IX_Floor_Name",
                table: "Floor");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Towns_Name",
                table: "Towns",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PropertyType_Name",
                table: "PropertyType",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo",
                columns: new[] { "FirstName", "MiddleName", "LastName" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Floor_Name",
                table: "Floor",
                column: "Name");
        }
    }
}
