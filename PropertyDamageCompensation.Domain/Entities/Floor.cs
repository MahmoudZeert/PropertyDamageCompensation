﻿namespace PropertyDamageCompensation.Domain.Entities
{
    public class Floor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Application>? Application { get; set; }
    }
}
