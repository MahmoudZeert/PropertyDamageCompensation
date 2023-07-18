using PropertyDamageCompensation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Domain.Entities
{
    public class DistrictEngineer
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
        public string EngineerId { get; set; } = String.Empty;
        public IApplicationUserExtending ApplicationUser { get; set; }
    }
}
