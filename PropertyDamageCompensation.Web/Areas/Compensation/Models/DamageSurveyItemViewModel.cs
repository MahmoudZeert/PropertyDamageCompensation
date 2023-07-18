using Microsoft.AspNetCore.Components.Forms;
using PropertyDamageCompensation.Web.Areas.Compensation.Entities;
using System;

namespace PropertyDamageCompensation.Web.Areas.Compensation.Models
{
    //these classes will be used
    // to receive a data object from javascript ManageDamageSurvey.js as a result of ajax call
    public class DamageSurveyItemViewModel
    {
        public class DamageAssessedItem
        {
            public int Id { get; set; }
            public int DamageItemDefId { get; set; }
            public string DamageItemdefName { get; set; } = string.Empty;
            public int DamageItemDefPrice { get; set; }
            public int Qty { get; set; }
            public int TotalAmount { get; set; }
        }
        public int DamageSurveyId { get; set; }
        public DateTime Date { get; set; }
        public int ApplicationId { get; set; }
        public int PersonalInfoId { get; set; }
        public string EngineerId { get; set; } = string.Empty;
        public int TotalAmount { get; set; }
        public DamageAssessedItem? ItemAssessed { get; set; }
    }
}
