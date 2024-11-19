using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_USERRIGHTMST
    {
        [Key]
        public int URTMstCode { get; set; }

        public int ModId { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDT { get; set; }
        public DateTime LMDT { get; set; }
        public string LMBY { get; set; }
    }
}
