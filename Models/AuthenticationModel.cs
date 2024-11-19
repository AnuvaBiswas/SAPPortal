namespace SAPPortal.Models
{
    public class AuthenticationModel
    {
        public long MenuId { get; set; }
        public string? VIEW { get; set; }
        public string? EDIT { get; set; }
        public string? ADD { get; set; }
        public string? PRINT { get; set; }
        public string? DELETE { get; set; }
    }
}
