namespace SAPPortal.Models
{
    public class BatchModel
    {
        public string? BatchNumber { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? DocEntry { get; set; }
        public string? DocNumer { get; set; }
        public string? PurchaseOrderNumer { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? WhsName { get; set; }
        public string? Project { get; set; }
        public double? Quantity { get; set; } 
    }
}
