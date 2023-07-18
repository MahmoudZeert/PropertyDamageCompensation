using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Identity.Models
{
    public class OrganizationUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }
        public List<string>? UserRoles  { get; set; }

    }
}
