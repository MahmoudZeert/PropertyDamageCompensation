using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Entities
{
    public class Town
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [InverseProperty("PlaceOfBirthTown")]
        public ICollection<PersonalInfo>? PlaceOfBirthTownPersonalInfo { get; set; }
        [InverseProperty("LivingTown")]
        public ICollection<PersonalInfo>? LIvingTownPersonalInfo { get; set; }
        [DefaultValue("1")]
        public int DistrictId { get; set; } = 1;
        [ForeignKey("DistrictId")]
        public District? District { get; set; }
    }
}
