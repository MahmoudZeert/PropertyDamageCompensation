using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class ApplicationStateViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; } = String.Empty;
        [Display(Name ="Is Default")]
        public bool IsDefault { get; set; } = false;
    }
}
