namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public ICollection<Town>?    Towns { get; set; }
        public ICollection<DistrictEngineer>? DistrictEngineers { get; set; }
    }
}
