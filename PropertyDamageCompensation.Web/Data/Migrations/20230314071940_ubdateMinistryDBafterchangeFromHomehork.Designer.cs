﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyDamageCompensation.Web.Data;

#nullable disable

namespace PropertyDamageCompensation.Web.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230314071940_ubdateMinistryDBafterchangeFromHomehork")]
    partial class ubdateMinistryDBafterchangeFromHomehork
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AppDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Appartment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Block")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FloorId")
                        .HasColumnType("int");

                    b.Property<int>("Ikar")
                        .HasColumnType("int");

                    b.Property<int>("PersonalInfoId")
                        .HasColumnType("int");

                    b.Property<int>("PropertTypeId")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubIkar")
                        .HasColumnType("int");

                    b.Property<int>("TownId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FloorId");

                    b.HasIndex("PersonalInfoId");

                    b.HasIndex("PropertTypeId");

                    b.HasIndex("StateId");

                    b.HasIndex("TownId");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.ApplicationState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ApplicationState");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Floor");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PersonalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LivingTownId")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlaceOfBirthId")
                        .HasColumnType("int");

                    b.Property<int>("PlaceOfBirthTownId")
                        .HasColumnType("int");

                    b.Property<string>("RegisterId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LivingTownId");

                    b.HasIndex("PlaceOfBirthId");

                    b.HasIndex("UserId");

                    b.HasIndex("FirstName", "MiddleName", "LastName")
                        .IsUnique();

                    b.ToTable("PersonalInfo");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("PropertyType");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PropertyDamageCompensation.Web.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PropertyDamageCompensation.Web.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PropertyDamageCompensation.Web.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Application", b =>
                {
                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Floor", "Floor")
                        .WithMany("Application")
                        .HasForeignKey("FloorId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PersonalInfo", "PersonalInfo")
                        .WithMany("Application")
                        .HasForeignKey("PersonalInfoId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PropertyType", "PropertType")
                        .WithMany("Application")
                        .HasForeignKey("PropertTypeId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.ApplicationState", "AppState")
                        .WithMany("Application")
                        .HasForeignKey("StateId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .IsRequired();

                    b.Navigation("AppState");

                    b.Navigation("Floor");

                    b.Navigation("PersonalInfo");

                    b.Navigation("PropertType");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PersonalInfo", b =>
                {
                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Town", "LivingTown")
                        .WithMany("LIvingTownPersonalInfo")
                        .HasForeignKey("LivingTownId")
                        .IsRequired();

                    b.HasOne("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Town", "PlaceOfBirthTown")
                        .WithMany("PlaceOfBirthTownPersonalInfo")
                        .HasForeignKey("PlaceOfBirthId");

                    b.HasOne("PropertyDamageCompensation.Web.Data.ApplicationUser", "ApplicationUser")
                        .WithMany("PersonalInfo")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("LivingTown");

                    b.Navigation("PlaceOfBirthTown");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.ApplicationState", b =>
                {
                    b.Navigation("Application");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Floor", b =>
                {
                    b.Navigation("Application");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PersonalInfo", b =>
                {
                    b.Navigation("Application");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.PropertyType", b =>
                {
                    b.Navigation("Application");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Areas.Compensation.Entities.Town", b =>
                {
                    b.Navigation("LIvingTownPersonalInfo");

                    b.Navigation("PlaceOfBirthTownPersonalInfo");
                });

            modelBuilder.Entity("PropertyDamageCompensation.Web.Data.ApplicationUser", b =>
                {
                    b.Navigation("PersonalInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
