using System.ComponentModel.DataAnnotations;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    public class EngineerDistrictsViewModel
    {
        public int DistrictId { get; set; }

        public string DistrictName { get; set; } = String.Empty;

        public bool IsSelected { get; set; }
    }
}
