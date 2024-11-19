using System.ComponentModel.DataAnnotations.Schema;

namespace SAPPortal.Models
{
    public class Item_Parameter_Details_Model
    {
        public string? DocEntry { get; set; }
        public string? DocNum { get; set; }
        public string? LineNum { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? ProcessStage { get; set; }
        public string? ProcessStageName { get; set; }
        public string? ParmCode { get; set; }
        public string? ParmDesc { get; set; }
        public string? ParmType { get; set; }
        public string? Sequence { get; set; }
        public string? Mandatory { get; set; } 
        public string? StandValue { get; set; }
        public string? AcceptMaxValue { get; set; }
        public string? AcceptMinValue { get; set; }
        public string? HoldMaxValue { get; set; }
        public string? HoldMinValue { get; set; }
        public string? RejMaxValue { get; set; }
        public string? RejMinValue { get; set; }
        public string? ShowOnPrint { get; set; }
       // public string? AfterBeforeGRN { get; set; }
        //public string? ShowOnPO { get; set; }

        [NotMapped]
        public bool RequiredEdit { get; set; }
        // Backup fields for original values
        [NotMapped]
        public Item_Parameter_Details_Model? OriginalValues { get; set; }

        [NotMapped]
        public bool IsEdit { get; set; } = false; // New property to track edit mode
    }
}
