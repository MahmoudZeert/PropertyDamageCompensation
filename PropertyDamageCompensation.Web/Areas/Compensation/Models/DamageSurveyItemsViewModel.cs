using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class DamageSurveyItemsViewModel
    {
        public int DamageSurveyId { get; set; }
        [Required]
        public DateTime Date { get; set; } 
        public int ApplicationId { get; set; }
        public int PersonalInfoId { get; set; }
        public string EngineerId { get; set; }
        public List<DamageItemAssessedViewModel>?   ListOfDamageItems { get; set; } 
    }
}
