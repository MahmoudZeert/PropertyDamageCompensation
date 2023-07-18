using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class addDamageSurveyDamageItemDefDamageItemAssessedandrelatedupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DamageItemDef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageItemDef", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DamageSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    PersonalInfoId = table.Column<int>(type: "int", nullable: false),
                    EngineerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamageSurvey_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamageSurvey_AspNetUsers_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamageSurvey_PersonalInfo_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DamageItemAssessed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DamageItemDefId = table.Column<int>(type: "int", nullable: false),
                    DamageItemDefPrice = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<short>(type: "smallint", nullable: false),
                    DamgeSurveyId = table.Column<int>(type: "int", nullable: false),
                    DamageSurveyId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageItemAssessed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DamageItemAssessed_DamageItemDef_DamageItemDefId",
                        column: x => x.DamageItemDefId,
                        principalTable: "DamageItemDef",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DamageItemAssessed_DamageSurvey_DamageSurveyId",
                        column: x => x.DamageSurveyId,
                        principalTable: "DamageSurvey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamageItemAssessed_DamageItemDefId",
                table: "DamageItemAssessed",
                column: "DamageItemDefId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageItemAssessed_DamageSurveyId",
                table: "DamageItemAssessed",
                column: "DamageSurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageItemDef_Name",
                table: "DamageItemDef",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DamageSurvey_ApplicationId",
                table: "DamageSurvey",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageSurvey_EngineerId",
                table: "DamageSurvey",
                column: "EngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageSurvey_PersonalInfoId",
                table: "DamageSurvey",
                column: "PersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageItemAssessed");

            migrationBuilder.DropTable(
                name: "DamageItemDef");

            migrationBuilder.DropTable(
                name: "DamageSurvey");
        }
    }
}
