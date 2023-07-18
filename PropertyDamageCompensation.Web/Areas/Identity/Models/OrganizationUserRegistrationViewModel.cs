using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Identity.Models
{
    public class OrganizationUserRegistrationViewModel
    {
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; } = String.Empty;
        //public bool AcceptTerms { get; set; }
    }
}
