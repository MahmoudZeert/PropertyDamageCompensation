using PropertyDamageCompensation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
    public class DamageSurvey
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }
        public int PersonalInfoId { get; set; }
        [ForeignKey("PersonalInfoId")]
        public PersonalInfo? PersonalInfo { get; set; }
        public string? EngineerId { get; set; }
        [ForeignKey("EngineerId")]
        public IApplicationUserExtending? ApplicationUser { get; set; }
        public ICollection<DamageItemAssessed>? DamageItemAssessed { get; set; }
    }
}
