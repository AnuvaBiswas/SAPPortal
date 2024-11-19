using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_GROUPRIGHTDTL
    {
        [Key]
        public int GRTDtlCode { get; set; }
        public int GRTMstCode { get; set; }
        public int MenuId {  get; set; }
        public int URTCode { get; set; }
    }
}
