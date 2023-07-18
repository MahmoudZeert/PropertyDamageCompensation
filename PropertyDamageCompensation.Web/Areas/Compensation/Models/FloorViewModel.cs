using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class FloorViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; } = String.Empty;
    }
}
