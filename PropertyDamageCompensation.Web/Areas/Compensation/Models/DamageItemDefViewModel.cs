using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class DamageItemDefViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1,3000)]
        public int UnitPrice { get; set; }
    }
}
