using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_USERRIGHTDTL
    {
        [Key]
        public int URTDtlCode { get; set; }
        public int URTMstCode { get; set; }
        public int MenuId { get; set; }
        public int URTCode { get; set; }
    }
}
