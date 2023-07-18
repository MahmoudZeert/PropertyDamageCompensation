namespace PropertyDamageCompensation.Domain.EntitiesCopy
{
    public class ApplicationState
    {
        public int Id { get; set; }
        public String Name { get; set; } = String.Empty;
        public bool IsDefault { get; set; } = false;
        public ICollection<Application>? Application { get; set; }
    }
}
