using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_USERRIGHTTYPE
    {
        [Key] 
        public int URTCode { get; set; }
        public string Descr { get; set; }
        public Char Active { get; set; }
    }
}
