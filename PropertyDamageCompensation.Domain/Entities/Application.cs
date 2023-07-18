using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace PropertyDamageCompensation.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int PersonalInfoId { get; set; }
        public DateTime AppDate { get; set; }
        public string AppNumber { get; set; } = string.Empty;
        public PersonalInfo? PersonalInfo { get; set; }
        public int PropertTypeId { get; set; }
        public PropertyType? PropertType { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string Block { get; set; } = string.Empty;
        public int FloorId { get; set; }
        public Floor? Floor { get; set; }
        public string Appartment { get; set; } = string.Empty;
        public int Ikar { get; set; }
        public int SubIkar { get; set; }
        public int TownId { get; set; }
        public Town? Town { get; set; }
        public int StateId { get; set; }
        public ApplicationState? AppState { get; set; }
        public ICollection<DamageSurvey>? DamageSurvey { get; set; }
    }
}
