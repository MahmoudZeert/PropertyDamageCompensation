using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class ubdateMinistryDBafterchangeFromHomehork1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Towns_Name",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo");

            migrationBuilder.DropIndex(
                name: "IX_Floor_Name",
                table: "Floor");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationState_Name",
                table: "ApplicationState");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Towns_Name",
                table: "Towns",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo",
                columns: new[] { "FirstName", "MiddleName", "LastName" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Floor_Name",
                table: "Floor",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ApplicationState_Name",
                table: "ApplicationState",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Towns_Name",
                table: "Towns");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_PersonalInfo_FirstName_MiddleName_LastName",
                table: "PersonalInfo");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Floor_Name",
                table: "Floor");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ApplicationState_Name",
                table: "ApplicationState");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_Name",
                table: "Towns",
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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationState_Name",
                table: "ApplicationState",
                column: "Name",
                unique: true);
        }
    }
}
