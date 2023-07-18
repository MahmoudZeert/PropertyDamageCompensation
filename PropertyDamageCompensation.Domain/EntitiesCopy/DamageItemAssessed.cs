using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
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
}
