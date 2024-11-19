using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAPPortal.Models
{
    public class MenuStruct
    {

        public int ModId { get; set; }

        public string ModName { get; set; }

        public int MenuId { get; set; }

        public string? MenuText { get; set; }

        public string? Description { get; set; }

        public int ParentId { get; set; }

        public string? URL { get; set; }
        public char? MenuDisplay { get; set; }

        public int SlNo { get; set; }

        public int DisplayMode { get; set; }

        public int Depth { get; set; }
        public string? ADD { get; set; }
        public string? EDIT { get; set; }
        public string? VIEW { get; set; }

        public string? VIEWURTCode { get; set; }

        public string? EDITURTCode { get; set; }
        public string? ADDURTCode { get; set; }
        public bool IsViewChecked { get; set; }
        public bool IsAddChecked { get; set; }
        public bool IsEditChecked { get; set; }

    }
}
