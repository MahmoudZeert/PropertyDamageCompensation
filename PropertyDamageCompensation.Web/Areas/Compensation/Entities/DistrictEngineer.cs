using PropertyDamageCompensation.Web.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Entities
{
    public class DistrictEngineer
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
        public string EngineerId { get; set; } = String.Empty;
        [ForeignKey("EngineerId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
