using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class updatedamageitemassessedentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DamgeSurveyId",
                table: "DamageItemAssessed");

            migrationBuilder.AlterColumn<int>(
                name: "DamageSurveyId",
                table: "DamageItemAssessed",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DamageSurveyId",
                table: "DamageItemAssessed",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DamgeSurveyId",
                table: "DamageItemAssessed",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
