using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    public partial class IntialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create "Admin" role if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES ('1', 'Admin', 'ADMIN')");
            // Create "Applicant" role if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES ('2', 'Applicant', 'APPLICANT')");

            // Create the admin user if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, LockoutEnabled, SecurityStamp, PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,AccessFailedCount,IsApplicant) " +
                                 "VALUES ('1', 'mahmoudyzeort@gmail.com', 'MAHMOUDYZEORT@GMAIL.COM', 'mahmoudyzeort@gmail.com', 'MAHMOUDYZEORT@GMAIL.COM', 1, 0, '', 'AQAAAAEAACcQAAAAEK+M5NtwcMFgKAhQ+TPU+EGL8c3NemOISUIAOFaiOOWqedLUaIOxDDO8Wph+blRm6A==','03575359',0,0,0,0)");

            // Assign the admin user to the "Admin" role if not already assigned
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('1', '1')");
        }


    }
}
