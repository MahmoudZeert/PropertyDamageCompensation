using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class adddistrictcolumntoTownentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Towns",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Towns_DistrictId",
                table: "Towns",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Towns_District_DistrictId",
                table: "Towns",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towns_District_DistrictId",
                table: "Towns");

            migrationBuilder.DropIndex(
                name: "IX_Towns_DistrictId",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Towns");
        }
    }
}
