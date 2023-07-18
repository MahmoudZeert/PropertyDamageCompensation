using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{

    public class PersonalInfoViewModel
    {

        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Display(Name ="First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth{ get; set; }

        public int PlaceOfBirthTownId { get; set; }
        [Display(Name ="Blace Of Birth")]
        public string? PlaceOfBirthTownName { get; set; }
        public string? RegisterId { get; set; }
        public string? Address { get; set; }
        public string? Street { get; set; }
        public string? Phone { get; set; }
        public int LivingTownId { get; set; }
        [Display(Name = "Living Place")]
        public string? LivingTownName { get; set; }


    }
}