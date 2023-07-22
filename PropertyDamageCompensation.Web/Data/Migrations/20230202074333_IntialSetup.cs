using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Xml.Linq;

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
            // Create "Employee" role if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES ('3', 'Employee', 'EMPLOYEE')");
            // Create "Engineer" role if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES ('4', 'Engineer', 'ENGINEER')");
            // Create "General Manager" role if it doesn't exist
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, [Name], NormalizedName) VALUES ('5', 'General Manager', 'GENERAL MANAGER')");

            // Create the admin user if it doesn't exist
            // to login to the system the password is Myz@23
            // You have to create this gmail account at gmail platform for the admin in order to be able to receive emails
            // But, you must use this password :  Myz@23, to login to the system because the passwordhash used is for this password !!!!
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, LockoutEnabled, SecurityStamp, PasswordHash,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,AccessFailedCount,IsApplicant) " +
                                 "VALUES ('1', 'adminpdc@gmail.com', 'ADMINPDC@GMAIL.COM', ''adminpdc@gmail.com', 'ADMINPDC@GMAIL.COM', 1, 0, '', 'AQAAAAEAACcQAAAAEK+M5NtwcMFgKAhQ+TPU+EGL8c3NemOISUIAOFaiOOWqedLUaIOxDDO8Wph+blRm6A==','',0,0,0,0)");

            // Assign the admin user to the "Admin" role if not already assigned
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('1', '1')");
            // Assign the admin user to the "Admin" role if not already assigned
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('1', '3')");

            // Add ApplicationState records
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Created', 1)");
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Submitted', 0)");
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Approved', 0)");
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Paid', 0)");
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Rejected', 0)");
            migrationBuilder.Sql("INSERT INTO ApplicationState (Name, IsDefault) VALUES ('Checked', 0)");
            // Add District records
            migrationBuilder.Sql("INSERT INTO District (Name) VALUES ('El Chouf')");
            migrationBuilder.Sql("INSERT INTO District (Name) VALUES ('Aley')");
            migrationBuilder.Sql("INSERT INTO District (Name) VALUES ('Beirut')");
            migrationBuilder.Sql("INSERT INTO District (Name) VALUES ('Baabda')");
            migrationBuilder.Sql("INSERT INTO District (Name) VALUES ('El Metn')");
            // Add Town records
            migrationBuilder.Sql("INSERT INTO Towns (Name,DistrictId) VALUES ('Beirut',3)");
            migrationBuilder.Sql("INSERT INTO Towns (Name,DistrictId) VALUES ('Aley',2)");
            migrationBuilder.Sql("INSERT INTO Towns (Name,DistrictId) VALUES ('Damour',1)");
            migrationBuilder.Sql("INSERT INTO Towns (Name,DistrictId) VALUES ('Baabda',4)");
            // Add PropertyType records
            migrationBuilder.Sql("INSERT INTO PropertyType (Name) VALUES ('Appartment')");
            migrationBuilder.Sql("INSERT INTO PropertyType (Name) VALUES ('House')");
            migrationBuilder.Sql("INSERT INTO PropertyType (Name) VALUES ('Shop')");
            // Add Floor records
            migrationBuilder.Sql("INSERT INTO Floor (Name) VALUES ('First')");
            migrationBuilder.Sql("INSERT INTO Floor (Name) VALUES ('second')");
            migrationBuilder.Sql("INSERT INTO Floor (Name) VALUES ('Third')");
            migrationBuilder.Sql("INSERT INTO Floor (Name) VALUES ('Fourth')");
            migrationBuilder.Sql("INSERT INTO Floor (Name) VALUES ('Fifth')");
            // Add DamageItemDef records
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice ) VALUES('Out Door', 150)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice )  VALUES('Metaphorical ceiling ', 700)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice ) VALUES('Painting works', 6)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice )  VALUES('Wooden door', 200)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice ) VALUES('Metal door', 400)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice ) VALUES('Tile works', 8)");
            migrationBuilder.Sql(" INSERT into DamageItemDef(Name , UnitPrice ) VALUES('Plombing works', 20)");



        }



    }
}
