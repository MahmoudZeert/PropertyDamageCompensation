using PropertyDamageCompensation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyDamageCompensation.Domain.Interfaces
{
    public interface IApplicationUserExtending
    {
         string?  FirstName { get; }
         string?  LastName { get;  }
         bool IsApplicant { get;  }
         string? NeihgberhoodName { get; }
        PersonalInfo? PersonalInfo { get; }
        ICollection<DistrictEngineer> DistrictEngineer { get; }
        ICollection<DamageSurvey> DamageSurvey { get; }
    }
}
