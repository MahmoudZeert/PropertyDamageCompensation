using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Identity.Models
{
    public class EngineerWithDistrictsViewModel
    {
        public string? EmgineerId { get; set; }
        public string? EngineerName { get; set; }
        [Display(Name = "Email Address")]
        public string? EngineerEmailAddress { get; set; }
        public List<string>? EngineerDistricts  { get; set; }

    }
}
