using System.ComponentModel.DataAnnotations;

namespace SAPPortal.Models
{
    public class M_GROUPRIGHTMST
    {
        [Key]
        public int GRTMstCode { get; set; }

        public int ModId { get; set; }
        public int GroupId { get; set; }
        public DateTime EntryDT { get; set; }
        public DateTime LMDT {  get; set; }
        public string LMBY { get; set; }



    }
}
