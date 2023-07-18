using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using PropertyDamageCompensation.Web.Areas.Compensation.Models;

namespace PropertyDamageCompensation.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Town> Towns { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<PropertyDamageCompensation.Web.Areas.Compensation.Entities.Application> Application { get; set; }
        public DbSet<PropertyType> PropertyType { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<ApplicationState> ApplicationState { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<DistrictEngineer> DistrictEngineer { get; set; }
        public DbSet<DamageItemDef>? DamageItemDef { get; set; }
        public DbSet<DamageItemAssessed> DamageItemAssessed { get; set; }
        public DbSet<DamageSurvey> DamageSurvey { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationState>()
                .HasIndex(aps => aps.Name).IsUnique();              
            builder.Entity<Town>()
                .HasIndex(t => t.Name).IsUnique();
            builder.Entity<PropertyType>()
                .HasIndex(pt => pt.Name).IsUnique();
            builder.Entity<Floor>()
                .HasIndex(fl => fl.Name).IsUnique();
            builder.Entity<PersonalInfo>()
                .HasIndex(pi => new { pi.FirstName, pi.MiddleName ,pi.LastName }).IsUnique();
            builder.Entity<ApplicationUser>()
                .HasIndex(apu => new { apu.FirstName, apu.LastName }).IsUnique();
            builder.Entity<District>()
                .HasIndex(d => d.Name).IsUnique();
            builder.Entity<DamageItemDef>()
                .HasIndex(d => d.Name).IsUnique();

            foreach (var foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

    }
}