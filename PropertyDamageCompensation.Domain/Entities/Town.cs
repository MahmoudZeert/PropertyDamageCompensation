using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Domain.Entities
{
    public class Town
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PersonalInfo>? PlaceOfBirthTownPersonalInfo { get; set; }
        public ICollection<PersonalInfo>? LIvingTownPersonalInfo { get; set; }
        public int DistrictId { get; set; } = 1;
        public District? District { get; set; }
        public ICollection<Application>? Application { get; set; }  
    }
}
