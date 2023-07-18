using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
    internal class AllEntitties

    {
        public class Floor
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public ICollection<Application>? Application { get; set; }
        }

        public class ApplicationState
        {
            public int Id { get; set; }
            public String Name { get; set; } = String.Empty;
            public bool IsDefault { get; set; } = false;
            public ICollection<Application>? Application { get; set; }
        }
        public class Application
        {
            public int Id { get; set; }
            public int PersonalInfoId { get; set; }
            public DateTime AppDate { get; set; }
            public string AppNumber { get; set; } = string.Empty;
            [ForeignKey("PersonalInfoId")]
            public PersonalInfo? PersonalInfo { get; set; }
            public int PropertTypeId { get; set; }
            [ForeignKey("PropertTypeId")]
            public PropertyType? PropertType { get; set; }
            public string Street { get; set; } = string.Empty;
            public string Building { get; set; } = string.Empty;
            public string Block { get; set; } = string.Empty;
            public int FloorId { get; set; }
            [ForeignKey("FloorId")]
            public Floor? Floor { get; set; }
            public string Appartment { get; set; } = string.Empty;
            public int Ikar { get; set; }
            public int SubIkar { get; set; }
            public int TownId { get; set; }
            [ForeignKey("TownId")]
            public Town? Town { get; set; }
            public int StateId { get; set; }
            [ForeignKey("StateId")]
            public ApplicationState? AppState { get; set; }
            public ICollection<DamageSurvey>? DamageSurvey { get; set; }
        }

        public class DamageItemAssessed
        {
            public int Id { get; set; }
            public int DamageItemDefId { get; set; }
            [ForeignKey("DamageItemDefId")]
            public DamageItemDef? DamageItemDef { get; set; }
            public int DamageItemDefPrice { get; set; }
            public short Qty { get; set; }
            public int DamageSurveyId { get; set; }
            [ForeignKey("DamageSurveyId")]
            public DamageSurvey? DamageSurvey { get; set; }
            public int TotalAmount { get; set; }

        }
        public class DamageItemDef
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int UnitPrice { get; set; }
            public ICollection<DamageItemAssessed>? DamageItemAssessed { get; set; }
        }
        public class DamageSurvey
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int ApplicationId { get; set; }
            [ForeignKey("ApplicationId")]
            public Application? Application { get; set; }
            public int PersonalInfoId { get; set; }
            [ForeignKey("PersonalInfoId")]
            public PersonalInfo? PersonalInfo { get; set; }
            public string? EngineerId { get; set; }
            // [ForeignKey("EngineerId")]
            // public IApplicationUserExtending? ApplicationUser { get; set; }
            public ICollection<DamageItemAssessed>? DamageItemAssessed { get; set; }
        }
        public class District
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            //public ICollection<Town>?    Towns { get; set; }
            public ICollection<DistrictEngineer>? DistrictEngineers { get; set; }
        }

        public class DistrictEngineer
        {
            public int Id { get; set; }
            public int DistrictId { get; set; }
            [ForeignKey("DistrictId")]
            public District? District { get; set; }
            public string EngineerId { get; set; } = String.Empty;
            //  [ForeignKey("EngineerId")]
            // public IApplicationUserExtending? ApplicationUser { get; set; }
        }

        public class PersonalInfo
        {
            public int Id { get; set; }
            public string UserId { get; set; } = string.Empty;
            //   [ForeignKey("UserId")]
            // public IApplicationUserExtending? ApplicationUser { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string MiddleName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public int PlaceOfBirthTownId { get; set; }
            [ForeignKey("PlaceOfBirthId")]
            public Town? PlaceOfBirthTown { get; set; }
            public string? RegisterId { get; set; }
            public string? Address { get; set; }
            public string? Street { get; set; }
            public string? Phone { get; set; }
            public int LivingTownId { get; set; }
            [ForeignKey("LivingTownId")]
            public Town? LivingTown { get; set; }
            public ICollection<Application>? Application { get; set; }


        }
        public class PropertyType
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public ICollection<Application>? Application { get; set; }
        }


    }
}
