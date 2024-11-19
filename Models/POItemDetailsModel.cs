using System.ComponentModel;

namespace SAPPortal.Models
{
    public class POItemDetailsModel
    {
        public string? DocEntry { get; set; }
        public string? DocNum { get; set; }
        public string? LineNum { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? TaxCode { get; set; }
        public string? WarehouseCode { get; set; }
        public DateTime PostingDate { get; set; }
    }
}
