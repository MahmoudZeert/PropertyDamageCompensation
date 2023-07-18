using PropertyDamageCompensation.Web.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Entities
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string MiddleName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int PlaceOfBirthTownId { get; set; }
        [ForeignKey("PlaceOfBirthId")]
        public Town? PlaceOfBirthTown { get; set; }
        public string? RegisterId { get; set; }
        public string? Address { get; set; }
        public string? Street { get; set; }
        public string? Phone { get; set; }
        public int LivingTownId { get; set; }
        [ForeignKey("LivingTownId")]
        public Town? LivingTown { get; set; }
        public ICollection<Application> Application { get; set; }


    }
}
