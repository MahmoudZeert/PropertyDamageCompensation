using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Identity.Models
{
    public class UserLoginViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RemeberMe { get; set; }

        //public string? LoginInInvalid { get; set; }
    }
}
