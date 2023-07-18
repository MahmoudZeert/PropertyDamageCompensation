namespace PropertyDamageCompensation.Web.Areas.Compensation.Entities
{
    public class DamageItemDef
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public ICollection<DamageItemAssessed>? DamageItemAssessed { get; set; }
    }
}
