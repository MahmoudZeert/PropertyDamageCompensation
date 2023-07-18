using PropertyDamageCompensation.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
    public class DistrictEngineer
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
        public string EngineerId { get; set; } = String.Empty;
      //  [ForeignKey("EngineerId")]
       // public IApplicationUserExtending? ApplicationUser { get; set; }
    }
}
