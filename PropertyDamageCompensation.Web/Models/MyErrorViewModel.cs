namespace PropertyDamageCompensation.Web.Models
{
    public class MyErrorViewModel
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; } = String.Empty;
    }
}