namespace PropertyDamageCompensation.Domain.Exceptions
{
    public class KeyNotFoundException:CustomException
    {
        public KeyNotFoundException(string message) : base(message) { }
    }
}
