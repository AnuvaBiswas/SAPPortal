namespace SAPPortal.Models
{
    public class ItemModel
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGroupName { get; set; }
        public string? DocEntry { get; set; }
        public string? DocNumer {  get; set; }
        public string? LineNum {  get; set; }
        public string? PurchaseOrderNumer { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? WhsName { get; set; }
        public string? WhsCode { get; set; }
        public string? Project { get; set; }
        public string? ProjectName { get; set; }
    }
}
