using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAPPortal.Models
{
    public class M_MENU
    {
        [Key]
        public int MenuId { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string? ModName { get; set; }
    
        public int ModuleID { get; set; }

        public int VIEWURTCode { get; set; }
        public int EDITURTCode { get; set; }
        public int ADDURTCode { get; set; }
        public bool IsViewChecked { get; set; }
        public bool IsAddChecked { get; set; }
        public bool IsEditChecked { get; set; }
        //[Key]
        //public int ModuleId { get; set; }
        //public string? ModName { get; set; }

        //public int MenuId { get; set; }

        //public string? MenuText { get; set; }

        //public string? Description { get; set; }

        //public int ParentId { get; set; }

        //public string? URL { get; set; }
        //public char? MenuDisplay { get; set; }

        //public int SlNo { get; set; }

        public int DisplayMode { get; set; }

        //public int Depth { get; set; }
    }
}
