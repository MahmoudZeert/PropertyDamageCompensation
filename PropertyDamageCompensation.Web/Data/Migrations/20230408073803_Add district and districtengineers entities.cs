using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class Adddistrictanddistrictengineersentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Floor_FloorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_PersonalInfo_PersonalInfoId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_PropertyType_PropertTypeId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Towns_TownId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Towns_LivingTownId",
                table: "PersonalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Towns_PlaceOfBirthId",
                table: "PersonalInfo");

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictEngineer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    EngineerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictEngineer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictEngineer_AspNetUsers_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistrictEngineer_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_District_Name",
                table: "District",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DistrictEngineer_DistrictId",
                table: "DistrictEngineer",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictEngineer_EngineerId",
                table: "DistrictEngineer",
                column: "EngineerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application",
                column: "StateId",
                principalTable: "ApplicationState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Floor_FloorId",
                table: "Application",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_PersonalInfo_PersonalInfoId",
                table: "Application",
                column: "PersonalInfoId",
                principalTable: "PersonalInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_PropertyType_PropertTypeId",
                table: "Application",
                column: "PropertTypeId",
                principalTable: "PropertyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Towns_TownId",
                table: "Application",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Towns_LivingTownId",
                table: "PersonalInfo",
                column: "LivingTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Towns_PlaceOfBirthId",
                table: "PersonalInfo",
                column: "PlaceOfBirthId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Floor_FloorId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_PersonalInfo_PersonalInfoId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_PropertyType_PropertTypeId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_Towns_TownId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Towns_LivingTownId",
                table: "PersonalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInfo_Towns_PlaceOfBirthId",
                table: "PersonalInfo");

            migrationBuilder.DropTable(
                name: "DistrictEngineer");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationState_StateId",
                table: "Application",
                column: "StateId",
                principalTable: "ApplicationState",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Floor_FloorId",
                table: "Application",
                column: "FloorId",
                principalTable: "Floor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_PersonalInfo_PersonalInfoId",
                table: "Application",
                column: "PersonalInfoId",
                principalTable: "PersonalInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_PropertyType_PropertTypeId",
                table: "Application",
                column: "PropertTypeId",
                principalTable: "PropertyType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Towns_TownId",
                table: "Application",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_AspNetUsers_UserId",
                table: "PersonalInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Towns_LivingTownId",
                table: "PersonalInfo",
                column: "LivingTownId",
                principalTable: "Towns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInfo_Towns_PlaceOfBirthId",
                table: "PersonalInfo",
                column: "PlaceOfBirthId",
                principalTable: "Towns",
                principalColumn: "Id");
        }
    }
}
