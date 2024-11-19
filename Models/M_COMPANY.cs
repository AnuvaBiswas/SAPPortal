using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_COMPANY
    {
        [Key]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanySAPDBName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
