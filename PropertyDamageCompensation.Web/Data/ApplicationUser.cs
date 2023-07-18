using Microsoft.AspNetCore.Identity;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;

namespace PropertyDamageCompensation.Web.Data
{

    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public bool IsApplicant { get; set; } = true;
        public ICollection<PersonalInfo>? PersonalInfo { get; set; }
        public ICollection<DistrictEngineer> DistrictEngineer { get; set; }
        public ICollection<DamageSurvey> DamageSurvey { get; set; }
    }
}
