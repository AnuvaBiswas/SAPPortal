using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_MODULE
    {
        [Key]
        public int ModId { get; set; }
        public string? ModName { get; set; }
        public string? Descr {  get; set; }
        public int? SlNo { get; set; }
    }
}
