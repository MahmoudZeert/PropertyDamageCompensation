using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class ApplicationViewModel
    {

        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        [Display(Name ="Application Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)]
        public DateTime AppDate { get; set; }
        [Display(Name ="Application Number")]
        public string AppNumber { get; set; } = string.Empty;
        [Display(Name ="Full Name")]
        public string FullName { get; set; } = String.Empty;
        [Display(Name ="Address Info")]
        public string AddressStreetLivingTown { get; set; } = String.Empty;
        [Display(Name = "Property Type")]
        public int PropertTypeId { get; set; }
        [Display(Name = "Property Type")]
        public string PropertTypeName { get; set; }=String.Empty;
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string Block { get; set; } = string.Empty;
        [Display(Name = "Floor")]
        public int FloorId { get; set; }
        [Display(Name = "Floor 1")]
        public string FloorName { get; set; } = string.Empty;
        public string Appartment { get; set; } = string.Empty;
        public int Ikar { get; set; }
        [Display(Name = "S.Ikar")]
        public int SubIkar { get; set; }
        [Display(Name = "Town")]
        public int TownId { get; set; }
        [Display(Name = "Town")]
        public string TownName { get; set; } = string.Empty;
        [Display(Name = "App State")]
        public int StateId { get; set; }
        [Display(Name = "App State")]
        public string ApplicationState  { get; set; } = string.Empty;
    }
}
