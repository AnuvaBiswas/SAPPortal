namespace SAPPortal.Models
{
    public class UseGrpStructure
    {
        public int GRTMstCode { get; set; }
        public int ModId { get; set; }
        public int GroupId { get; set; }

        public DateTime EntryDT { get; set; }
        public DateTime LMDT { get; set; }
        public int LMBY { get; set; }
        public int MenuId { get; set; }
        public int URTCode { get; set; }
        public string Descr { get;  set; }
        public string ModName { get; set; }
        public int VIEWURTCode { get; set; }
        public int EDITURTCode { get; set; }
        public int ADDURTCode { get; set; }
        public bool IsViewChecked { get; set; }
        public bool IsAddChecked { get; set; }
        public bool IsEditChecked { get; set; }
    }
}
