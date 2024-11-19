using System.ComponentModel.DataAnnotations;
namespace SAPPortal.Models
{
    public class M_User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? PWD { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
        public string? ContactNo1 { get; set; }
        public string? ContactNo2 { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Pin { get; set; }
        public string? SAPId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? GroupId { get; set; }

        public  List<M_COMPANY> lstCompanies = new List<M_COMPANY>();

    }
}
