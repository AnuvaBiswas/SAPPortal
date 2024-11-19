using System.ComponentModel.DataAnnotations.Schema;

namespace SAPPortal.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string? FormName { get; set; }
        public string? Module { get; set; }
        [NotMapped]
        public string? IsAccessable { get; set; }
        [NotMapped]
        public bool IsAccessibleBool
        {
            get => IsAccessable == "Y";
            set => IsAccessable = value ? "Y" : "N";
        }

    }
}
