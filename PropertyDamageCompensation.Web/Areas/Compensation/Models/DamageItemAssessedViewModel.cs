using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models

{
    public class DamageItemAssessedViewModel
    {
        public int Id { get; set; }
        public int DamageItemDefId { get; set; }
        public string DamageItemdefName { get; set; } = string.Empty;
        public int DamageItemDefPrice { get; set; }
        public int Qty { get; set; }
        public int TotalAmount { get; set; }         
    }
}
