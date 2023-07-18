using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PropertyDamageCompensation.Domain.Entities;
using System.Reflection.Emit;

namespace PropertyDamageCompensation.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Town> Towns { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<Domain.Entities.Application> Application { get; set; }
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

            // configure Town
            builder.Entity<Town>()
                .HasIndex(t => t.Name).IsUnique();
            // configure ApplicationState entity
            builder.Entity<ApplicationState>()
                .HasKey(a => a.Id);
            builder.Entity<ApplicationState>()
                .HasIndex(a => a.Name)
                .IsUnique();
            //configure Floor entity
            builder.Entity<Floor>()
                .HasKey(f => f.Id);
            builder.Entity<Floor>()
                .HasIndex(f => f.Name)
                .IsUnique();
            // Configure propertytype entity
            builder.Entity<PropertyType>()
                .HasKey(pt => pt.Id);
            builder.Entity<District>()
                .HasIndex(pt => pt.Name)
                .IsUnique();
            // Configure propertytype entity
            builder.Entity<District>()
                .HasKey(d => d.Id);
            builder.Entity<District>()
                .HasIndex(d => d.Name)
                .IsUnique();
            // configure DamageItemDef
            builder.Entity<DamageItemDef>()
                .HasKey(d => d.Id);
            builder.Entity<DamageItemDef>()
                .HasIndex(d => d.Name)
                .IsUnique();
            // configure DistrictEngineer
            builder.Entity<DistrictEngineer>()
                .HasKey(de => de.Id);
            builder.Entity<DistrictEngineer>()
                .HasOne(de => de.District)
                .WithMany(d => d.DistrictEngineers)
                .HasForeignKey(de => de.DistrictId)
                .IsRequired();
            builder.Entity<DistrictEngineer>()
                .HasOne(de => (ApplicationUser)de.ApplicationUser)
                .WithMany(au => au.DistrictEngineer)
                .HasForeignKey(de => de.EngineerId)
                .IsRequired();
            // configure DamageItemAssessed
            builder.Entity<DamageItemAssessed>()
                .HasOne(dia => dia.DamageItemDef)
                .WithMany(did => did.DamageItemAssessed)
                .HasForeignKey(dia => dia.DamageItemDefId)
                .IsRequired();
            builder.Entity<DamageItemAssessed>()
                            .HasOne(dia => dia.DamageSurvey)
                            .WithMany(dis => dis.DamageItemAssessed)
                            .HasForeignKey(dia => dia.DamageSurveyId)
                            .IsRequired();
            // confifgure DamageSurvey
            builder.Entity<DamageSurvey>()
                .HasOne(ds => ds.Application)
                .WithMany(ap => ap.DamageSurvey)
                .HasForeignKey(ds => ds.ApplicationId)
                .IsRequired();
            builder.Entity<DamageSurvey>()
                .HasOne(ds => ds.PersonalInfo)
                .WithMany(pi => pi.DamageSurvey)
                .HasForeignKey(ds => ds.PersonalInfoId)
                .IsRequired();
            builder.Entity<DamageSurvey>()
                .HasOne(ds=>(ApplicationUser)ds.ApplicationUser)
                .WithMany(au => au.DamageSurvey)
                .HasForeignKey(ds=>ds.EngineerId)
                .IsRequired();
            // confifgure Application
            builder.Entity<Domain.Entities.Application>()
                .HasOne(ap => ap.PersonalInfo)
                .WithMany(pi => pi.Application)
                .HasForeignKey(ap => ap.PersonalInfoId)
                .IsRequired();
            builder.Entity<Domain.Entities.Application>()
                .HasOne(ap => ap.PropertType)
                .WithMany(pt => pt.Application)
                .HasForeignKey(ap => ap.PropertTypeId)
                .IsRequired();
            builder.Entity<Domain.Entities.Application>()
                .HasOne(ap => ap.Floor)
                .WithMany(fl => fl.Application)
                .HasForeignKey(ap => ap.FloorId)
                .IsRequired();
            builder.Entity<Domain.Entities.Application>()
                .HasOne(ap => ap.Town)
                .WithMany(pt => pt.Application)
                .HasForeignKey(ap => ap.TownId)
                .IsRequired();
            builder.Entity<Domain.Entities.Application>()
                .HasOne(ap => ap.AppState)
                .WithMany(pt => pt.Application)
                .HasForeignKey(ap => ap.StateId)
                .IsRequired();

            // configure PersonalInfo


            builder.Entity<PersonalInfo>()
                .HasIndex(pi => new { pi.FirstName, pi.MiddleName, pi.LastName }).IsUnique();
            builder.Entity<PersonalInfo>()
                .HasOne(pi => (ApplicationUser)pi.ApplicationUser)
                .WithOne(au => au.PersonalInfo)
                .HasForeignKey<PersonalInfo>(pi => pi.UserId)
                .IsRequired();
            builder.Entity<PersonalInfo>()
                .HasOne(pi => pi.PlaceOfBirthTown)
                .WithMany(to => to.PlaceOfBirthTownPersonalInfo)
                .HasForeignKey(pi=>pi.PlaceOfBirthTownId)
                .IsRequired();
            builder.Entity<PersonalInfo>()
                .HasOne(pi => pi.LivingTown)
                .WithMany(to => to.LIvingTownPersonalInfo)
                .HasForeignKey(pi => pi.LivingTownId)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .HasIndex(apu => new { apu.FirstName, apu.LastName }).IsUnique();
            foreach (var foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
        }

    }
}