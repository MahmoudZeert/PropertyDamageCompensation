namespace PropertyDamageCompensation.Domain.Entities
{
    public class PropertyType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Application>? Application { get; set; }
    }
}
