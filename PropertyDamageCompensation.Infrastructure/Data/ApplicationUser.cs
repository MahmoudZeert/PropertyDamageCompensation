using Microsoft.AspNetCore.Identity;
using PropertyDamageCompensation.Domain.Entities;
using PropertyDamageCompensation.Domain.Interfaces;

namespace PropertyDamageCompensation.Infrastructure.Data
{

    public class ApplicationUser : IdentityUser, IApplicationUserExtending
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public bool IsApplicant { get; set; } = true;
        public PersonalInfo? PersonalInfo { get; set; }
        public ICollection<DistrictEngineer> DistrictEngineer { get; set; }
        public ICollection<DamageSurvey> DamageSurvey { get; set; }
        public string? NeihgberhoodName { get; set; }
    }
}
