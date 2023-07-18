using PropertyDamageCompensation.Web.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Entities
{
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
        [ForeignKey("EngineerId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<DamageItemAssessed>? DamageItemAssessed { get; set; }
    }
}
