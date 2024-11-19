namespace SAPPortal.Models
{
    public class MenuMapUser
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int ? FormId { get; set; }
        public string? FormName { get; set; }
        public char? IsAccessable { get; set; }
    }
}
