using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class TownViewModel
    {
        [Key ]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        [Display(Name ="District Name")]
        public string DistrictName { get; set; }=String.Empty;

    }


}
