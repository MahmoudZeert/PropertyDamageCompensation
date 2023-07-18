using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Identity.Models
{
    public class RoleViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty; 
    }
}
