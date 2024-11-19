using System.ComponentModel.DataAnnotations;
namespace SAPPortal.Models
{
    public class M_USERGROUP
    {
        [Key]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? ParentGroup { get; set; }
        public char? IsActive { get; set; }
        public int? CreatedBy  { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CompanyCode { get; set; }
    }
}
